using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
	public class PagesQueryHandler : IMiddleware<PagesQuery, IEnumerable<Page>>
	{
		private IPageRepository _pageRepository;

		public PagesQueryHandler(
			IPageRepository pageRepository)
		{
			_pageRepository = pageRepository;
		}

		public Task<IEnumerable<Page>> HandleAsync(PagesQuery request, Func<Task<IEnumerable<Page>>> next, CancellationToken cancellationToken)
		{
			var filter = new PagesFilter()
			{
				ShowOnDrawer = request.ShowOnDrawer
			};

			var Pages = _pageRepository.GetMany(filter);

			return Pages;
		}
	}
}