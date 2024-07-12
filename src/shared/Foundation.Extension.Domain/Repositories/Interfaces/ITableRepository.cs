using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
  public interface ITableRepository
  {
    Task<IEnumerable<Table>> GetMany(TablesFilter filter);
    Task<Table> Get(Guid id);
    Task<Table> GetFromCode(string code);
  }
}