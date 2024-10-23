using System.Collections.Generic;
using System.Linq;
using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core
{
	public class UserOrganisationTableQuery : IRequest<UserOrganisationTableDetails>, ICoreRequest
	{
		public IEnumerable<string> Authorizations => Enumerable.Empty<string>();
		public string TableCode { get; set; }
	}
}