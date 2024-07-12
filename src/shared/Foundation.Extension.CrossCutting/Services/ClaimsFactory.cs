using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using static Foundation.Extension.Domain.ClaimsKeys;
using Foundation.Extension.Domain.Repositories.Interfaces;

using System.Linq;
using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.CrossCutting.Services
{
    public class ClaimsFactory : IClaimsFactory
    {
        public (Guid? UserId, Guid? SourceId, string RandomCookie, Guid ApplicationId, bool IsAdministrator, string LanguageCode) Get(IEnumerable<Claim> claims)
        {
            Guid temp;

            var userIdClaim = claims.SingleOrDefault(c => c.Type == USER_ID);
            Guid? userId = Guid.TryParse(userIdClaim?.Value, out temp) ? (Guid?)temp : null;

            var sourceIdClaim = claims.SingleOrDefault(c => c.Type == SOURCE_ID);
            Guid? sourceId = Guid.TryParse(sourceIdClaim?.Value, out temp) ? (Guid?)temp : null;

            var randomCookieClaim = claims.SingleOrDefault(c => c.Type == RANDOM_COOKIE);
            string randomCookie = randomCookieClaim?.Value;

            var applicationIdClaim = claims.SingleOrDefault(c => c.Type == APPLICATION_ID);
            Guid.TryParse(applicationIdClaim?.Value, out var applicationId);

            var isAdministratorClaim = claims.SingleOrDefault(c => c.Type == ADMINISTRATOR);
            Boolean.TryParse(isAdministratorClaim?.Value, out var isAdministrator);

            var languageClaim = claims.SingleOrDefault(c => c.Type == LANGUAGE_CODE);
            string languageCode = languageClaim?.Value;

            return (
                userId,
                sourceId,
                randomCookie,
                applicationId,
                isAdministrator,
                languageCode
            );
        }
    }
}