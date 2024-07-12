using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class TranslationsQueryHandler: IMiddleware<TranslationsQuery, IEnumerable<Translation>>
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationsQueryHandler(
            ITranslationRepository translationRepository
        )
        {
            _translationRepository = translationRepository;    
        }

        public async Task<IEnumerable<Translation>> HandleAsync(TranslationsQuery request, Func<Task<IEnumerable<Translation>>> next, CancellationToken cancellationToken)
        {
            return await _translationRepository.GetMany();
        }
    }
}