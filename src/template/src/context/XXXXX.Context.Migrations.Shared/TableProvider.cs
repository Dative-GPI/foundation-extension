using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Fixtures;
using System.Linq;

namespace XXXXX.Context.Migrations.Shared
{
  public static class TableProvider
  {
    static readonly List<string> PROJECTS = new List<string>()
        {
            "../../../src/app/admin/XXXXX.Admin.UI",
            "../../../src/app/core/XXXXX.Core.UI",
        };

    public static async Task<List<Fixture>> GetAllTables()
    {
      var tables = new List<Fixture>();

      foreach (var project in PROJECTS)
      {
        var table = await TableHelper.GetTables(project);
        tables.AddRange(table);
      }

      return tables.DistinctBy(t => t.Code).ToList();
    }
  }
}
