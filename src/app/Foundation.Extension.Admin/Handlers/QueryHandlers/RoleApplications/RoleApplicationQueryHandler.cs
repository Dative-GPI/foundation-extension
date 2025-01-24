using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class RoleApplicationQueryHandler : IMiddleware<RoleApplicationQuery, RoleApplicationDetails>
    {
        private readonly IRoleApplicationRepository _roleApplicationRepository;

        public RoleApplicationQueryHandler(IRoleApplicationRepository roleApplicationRepository)
        {
            _roleApplicationRepository = roleApplicationRepository;
        }

        public async Task<RoleApplicationDetails> HandleAsync(RoleApplicationQuery request, Func<Task<RoleApplicationDetails>> next, CancellationToken cancellationToken)
        {
            var roleApplication = await _roleApplicationRepository.Get(request.RoleApplicationId);
            
            return roleApplication;
        }
    }
}