using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Fixtures;

namespace XXXXX.Context.Migrations.Shared
{
  public static class TableProvider
  {
    static readonly List<string> PROJECTS = new List<string>()
        {
            "../../../src/app/admin/XXXXX.Admin.UI/src",
            "../../../src/app/core/XXXXX.Core.UI/src",
        };

    public static async Task<List<Table>> GetAllTables()
    {
      var tables = new List<Table>();

      foreach (var project in PROJECTS)
      {
        var table = await TableHelper.GetTables(project);
        tables.AddRange(table);
      }

      return tables.DistinctBy(t => t.Code).ToList();
    }
  }
}
