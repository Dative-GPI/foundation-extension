using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
  public interface IPageRepository
  {
    Task<IEnumerable<Page>> GetMany(PagesFilter filter);
  }
}