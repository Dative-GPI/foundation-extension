using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class EntityPropertiesQueryHandler : IMiddleware<EntityPropertiesQuery, IEnumerable<EntityProperty>>
    {
        private IEntityPropertyRepository _entityPropertyRepository;

        public EntityPropertiesQueryHandler(
            IEntityPropertyRepository entityPropertyRepository)
        {
            _entityPropertyRepository = entityPropertyRepository;
        }

        public Task<IEnumerable<EntityProperty>> HandleAsync(EntityPropertiesQuery request, Func<Task<IEnumerable<EntityProperty>>> next, CancellationToken cancellationToken)
        {
            var entityProperties = _entityPropertyRepository.GetMany(new EntityPropertiesFilter());

            return entityProperties;
        }
    }
}