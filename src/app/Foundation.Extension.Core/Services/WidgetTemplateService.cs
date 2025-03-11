using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;

using Foundation.Clients.Core.FoundationModels;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.AutoMapper;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.Services
{
    public class WidgetTemplateService : IWidgetTemplateService
    {
        private readonly RequestContext _requestContext;
        private readonly IQueryHandler<WidgetTemplateQuery, WidgetTemplateDetails> _widgetTemplateQueryHandler;
        private readonly IQueryHandler<WidgetTemplatesQuery, IEnumerable<WidgetTemplateInfos>> _widgetTemplatesQueryHandler;
        private readonly IMapper _mapper;

        public WidgetTemplateService
        (
            IRequestContextProvider requestContextProvider,
            IQueryHandler<WidgetTemplateQuery, WidgetTemplateDetails> widgetTemplateQueryHandler,
            IQueryHandler<WidgetTemplatesQuery, IEnumerable<WidgetTemplateInfos>> widgetTemplatesQueryHandler,
            IMapper mapper
        )
        {
            _requestContext = requestContextProvider.Context;
            _widgetTemplateQueryHandler = widgetTemplateQueryHandler;
            _widgetTemplatesQueryHandler = widgetTemplatesQueryHandler;
            _mapper = mapper;
        }

        /*public async Task<WidgetTemplateDetailsFoundationModel> Get(string languageCode, Guid widgetTemplateId)
        {
            var query = new WidgetTemplateQuery()
            {
                WidgetTemplateId = widgetTemplateId
            };

            var result = await _widgetTemplateQueryHandler.HandleAsync(query);

            return _mapper.Map<WidgetTemplateDetails, WidgetTemplateDetailsFoundationModel>(result, opt => opt.Items.Add(Consts.LANGUAGE, languageCode));
        }*/

        public async Task<IEnumerable<WidgetTemplateInfosFoundationModel>> GetMany(WidgetTemplatesFilterFoundationModel filter)
        {
            var query = new WidgetTemplatesQuery()
            {
                Search = filter.Search
            };
            
            var result = await _widgetTemplatesQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<WidgetTemplateInfos>, IEnumerable<WidgetTemplateInfosFoundationModel>>(result, opt => opt.Items.Add(Consts.LANGUAGE, _requestContext.LanguageCode));
        }
    }
}