using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation.Extension.Core.ViewModels;

namespace Foundation.Extension.Core.Abstractions
{
  public interface IUserOrganisationTableService
  {
    Task<UserOrganisationTableDetailsViewModel> Get(string tableCode);
    Task Update(string tableCode, UpdateUserOrganisationTableViewModel payload);
  }
}