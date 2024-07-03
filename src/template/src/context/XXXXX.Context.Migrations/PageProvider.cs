using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Template.Fixtures;
using System.Linq;
namespace XXXXX.Context.Migrations
{
    public static class PageProvider
    {
        public static Task<List<Page>> GetAllPages()
        {
            var coreRoutes = XXXXX.Core.Kernel.Routes.GetRoutes();

            var pages = coreRoutes.Select(r => new Page()
            {
                Code = r.Name,
                LabelDefault = r.DrawerLabelDefault,
                ShowOnDrawer = r.ShowOnDrawer
            }).ToList();
            
            return Task.FromResult(pages);
        }
    }
}
