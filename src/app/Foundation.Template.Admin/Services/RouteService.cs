using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;


using Foundation.Template.Domain.Models;

using Foundation.Template.Admin.Abstractions;
using Foundation.Template.Admin.Abstractions;
using Foundation.Template.Admin.ViewModels;

namespace Foundation.Template.Admin.Services
{
    public class RouteService : IRouteService
    {
        private IRoutesProvider _routesProvider;
        private IMapper _mapper;

        public RouteService(
            IRoutesProvider routesProvider,
            IMapper mapper
        )
        {
            _routesProvider = routesProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RouteInfosViewModel>> GetMany()
        {
            var result = await _routesProvider.GetRoutes();

            return _mapper.Map<IEnumerable<RouteInfos>, IEnumerable<RouteInfosViewModel>>(result);
        }
    }
}
