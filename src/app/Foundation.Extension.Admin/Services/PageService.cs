using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Foundation.Extension.Domain.Models;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Bones.Flow;

namespace Foundation.Extension.Admin.Services
{
  public class PageService : IPageService
  {
    private IMapper _mapper;
    private IQueryHandler<PagesQuery, IEnumerable<Page>> _pagesQueryHandler;

    public PageService(
            IMapper mapper,
            IQueryHandler<PagesQuery, IEnumerable<Page>> pagesQueryHandler
        )
    {
      _mapper = mapper;
      _pagesQueryHandler = pagesQueryHandler;
    }

    public async Task<IEnumerable<PageViewModel>> GetMany(PageFiltersViewModel payload)
    {
      var query = new PagesQuery()
      {
        ShowOnDrawer = payload.ShowOnDrawer
      };

      var pages = await _pagesQueryHandler.HandleAsync(query);

      return _mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(pages);
    }
  }
}
