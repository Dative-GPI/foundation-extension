using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Foundation.Template.Domain.Models;

using Foundation.Template.Core.Abstractions;
using Foundation.Template.Core.ViewModels;

namespace Foundation.Template.Core.Services
{
    public class ActionService : IActionService
    {
        private IActionsProvider _actionsProvider;
        private IMapper _mapper;

        public ActionService(
            IActionsProvider actionsProvider,
            IMapper mapper
        )
        {
            _actionsProvider = actionsProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActionInfosViewModel>> GetMany(ActionsFilterViewModel filter)
        {
            var result = await _actionsProvider.GetActions(filter.Path);

            return _mapper.Map<IEnumerable<ActionInfos>, IEnumerable<ActionInfosViewModel>>(result);
        }
    }
}
