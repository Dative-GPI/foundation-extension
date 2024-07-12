using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Abstractions
{
    public interface ITranslationsProvider
    {
        Task<IEnumerable<ApplicationTranslation>> GetMany(Guid applicationId, string languageCode);
    }
}