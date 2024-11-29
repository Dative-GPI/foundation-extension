using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using Foundation.Extension.Context.Abstractions;

using Foundation.Extension.Fixtures.Abstractions;

namespace Foundation.Extension.Fixtures
{
    public class BaseFixtureManager
    {
        public delegate Task<List<T>> FixtureGenerator<T>();
        private List<Func<bool, Task>> _generateActions = new List<Func<bool, Task>>();
        private List<Func<Task>> _listActions = new List<Func<Task>>();
        private List<Func<Task>> _verifyActions = new List<Func<Task>>();

        protected ILogger<BaseFixtureManager> Logger { get; private set; }
        protected IFixtureHelper FixtureHelper { get; private set; }

        public BaseFixtureManager(
            ILogger<BaseFixtureManager> logger,
            IFixtureHelper fixtureHelper)
        {
            Logger = logger;
            FixtureHelper = fixtureHelper;
        }

        public async Task Generate(bool dryRun = false)
        {
            Logger.LogInformation("Generate fixtures");

            foreach (var action in _generateActions)
            {
                await action(dryRun);
                Logger.LogInformation("------------------------");
            }
        }

        public async Task List()
        {
            Logger.LogInformation("List fixtures");

            foreach (var action in _listActions)
            {
                await action();
            }
        }

        public async Task Verify()
        {
            Logger.LogInformation("Verify fixtures");

            foreach (var action in _verifyActions)
            {
                await action();
            }
        }

        public void Add<TDTO, TPartial>(
            FixtureGenerator<TPartial> provider,
            Func<TPartial, TDTO> create,
            Func<TPartial, TDTO, TDTO> update = default)
            where TDTO : ICodeEntity
            where TPartial : ICodeEntity
        {
            if (update == default)
            {
                update = (partial, former) => former;
            }

            _generateActions.Add((dryRun) => Generate<TDTO, TPartial>(provider, create, update, dryRun));
            _listActions.Add(() => PrintInfos<TDTO>());
        }

        public void With<TDTO>()
        {
            _generateActions.Add((_) => PrintInfos<TDTO>());
            _listActions.Add(() => PrintInfos<TDTO>());
        }

        public void AddVerifier<TDTO, TEntity>(
            Func<IEnumerable<TEntity>, IEnumerable<TDTO>, ILogger, bool> check,
            FixtureGenerator<TEntity> provider = default)
            where TDTO : ICodeEntity
            where TEntity : ICodeEntity
        {
            _verifyActions.Add(() => Verify(provider, check, Logger));
        }

        private async Task PrintInfos<TDTO>()
        {
            var formers = await FixtureHelper.Get<TDTO>();
            Logger.LogInformation("Found {count} {type}", formers.Count(), typeof(TDTO).Name);
        }

        private async Task Generate<TDTO, TPartial>(
            FixtureGenerator<TPartial> provider,
            Func<TPartial, TDTO> create,
            Func<TPartial, TDTO, TDTO> update,
            bool dryRun = false)
            where TDTO : ICodeEntity
            where TPartial : ICodeEntity
        {
            var formers = await FixtureHelper.Get<TDTO>();

            Logger.LogInformation("{count} {type} existing", formers.Count(), typeof(TDTO).Name);

            var alls = await provider();

            Logger.LogInformation("{count} {type} actual", alls.Count(), typeof(TDTO).Name);

            var news = alls.ExceptBy(formers.Select(f => f.Code), a => a.Code).ToList();

            Logger.LogInformation("{count} {type} new", news.Count(), typeof(TDTO).Name);

            var saved = alls.Join(formers, a => a.Code, f => f.Code, (a, f) => update(a, f)) // formers still present
                .Concat(news.Select(n => create(n))) // + news
                .ToList();

            if (!dryRun)
                await FixtureHelper.Save<TDTO>(saved);

            Logger.LogInformation("{count} {type} saved", saved.Count(), typeof(TDTO).Name);
        }

        private async Task Verify<TDTO, TEntity>(
            FixtureGenerator<TEntity> provider,
            Func<IEnumerable<TEntity>, IEnumerable<TDTO>, ILogger, bool> check,
            ILogger logger)
            where TDTO : ICodeEntity
            where TEntity : ICodeEntity
        {
            var dtos = await FixtureHelper.Get<TDTO>();
            var entities = provider != null ? await provider() : null;

            var result = check(entities, dtos, logger);

            if(!result)
            {
                Logger.LogError("Verification failed for {type}", typeof(TDTO).Name);
                throw new Exception("Fixtures verification failed");
            }
             
            Logger.LogInformation("Verification passed for {type}", typeof(TDTO).Name);
        }
    }
}
