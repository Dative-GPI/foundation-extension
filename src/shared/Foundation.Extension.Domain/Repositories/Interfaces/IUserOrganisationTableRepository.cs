using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
	public interface IUserOrganisationTableRepository
	{
		Task<UserOrganisationTableDetails> Get(Guid id);
		Task<UserOrganisationTableDetails> Find(string tableCode, Guid userOrganisationId);
		Task<IEntity<Guid>> Create(CreateUserOrganisationTable payload);
		Task Remove(Guid userOrganisationColumnId);
	}
}