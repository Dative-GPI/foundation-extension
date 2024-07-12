using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Fixtures;
using System.Linq;
using System.Reflection;

using Foundation.Clients.Fixtures.Services;

namespace XXXXX.Context.Migrations
{
  public static class EntityPropertyProvider
  {
    static List<Assembly> Assemblies = new List<Assembly>() {
            XXXXX.Core.Kernel.KernelAssembly.Get(),
            XXXXX.Admin.Kernel.KernelAssembly.Get()
        };

    static List<string> Namespaces = new List<string>() {
            "XXXXX.Core.Kernel.ViewModels",
            "XXXXX.Admin.Kernel.ViewModels"
        };

    public static Task<List<EntityProperty>> GetAllEntityProperties()
    {
      var entityProperties = EntityPropertyHelper.GetAll(Assemblies, Namespaces);

      var result = entityProperties.DistinctBy(t => t.Code).ToList();

      return Task.FromResult(result);
    }
  }
}
