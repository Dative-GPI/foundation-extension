using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Foundation.Extension.Context.DTOs;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Repositories.Filters;

namespace Foundation.Extension.Context.Repositories
{
    public class WidgetTemplateRepository : IWidgetTemplateRepository
    {
        private readonly DbSet<WidgetTemplateDTO> _dbSet;

        public WidgetTemplateRepository(BaseApplicationContext context) 
        {
            _dbSet = context.WidgetTemplates;
        }

        public async Task<IEnumerable<WidgetTemplateInfos>> GetMany(WidgetTemplatesFilter filter)
        {
            var query = _dbSet
                .AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                //query = query.Where(w => w.SearchVector.Matches(EF.Functions.ToTsQuery(filter.Search.FormatForTsQuery())));
                query = query.Where(w => w.Search.Contains(filter.Search));
            }

            var dtos = await query.AsNoTracking().ToListAsync();

            return dtos.Select(widgetTemplateDTO => new WidgetTemplateInfos()
            {
                Id = widgetTemplateDTO.Id,
                Code = widgetTemplateDTO.Code,
                Icon = widgetTemplateDTO.Icon,
                Category = widgetTemplateDTO.Category,
                DefaultWidth = widgetTemplateDTO.DefaultWidth,
                DefaultHeight = widgetTemplateDTO.DefaultHeight,
                DefaultMeta = widgetTemplateDTO.DefaultMeta,
                Label = widgetTemplateDTO.LabelDefault,
                Description = widgetTemplateDTO.DescriptionDefault,
                Translations = widgetTemplateDTO.Translations?.Select(t => new TranslationWidgetTemplate()
                {
                    LanguageCode = t.LanguageCode,
                    Label = t.Label,
                    Description = t.Description
                }).ToList() ?? new List<TranslationWidgetTemplate>()
            });
        }
    }
}