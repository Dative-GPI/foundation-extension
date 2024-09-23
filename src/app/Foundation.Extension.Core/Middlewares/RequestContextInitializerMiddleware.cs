using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

using Foundation.Extension.Core.Tools;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Clients.Core.FoundationModels;

namespace Foundation.Extension.Core.Middlewares
{
	public class RequestContextInitializerMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestContextInitializerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var request = context.Request;
			var provider = context.RequestServices.GetRequiredService<RequestContextProvider>();

			var actorId = new Guid(request.Headers["X-User-Id"].ToString());
			var applicationId = new Guid(request.Headers["X-Application-Id"].ToString());

			string languageCode = null;
			if (request.Headers.TryGetValue("X-Language-Code", out var applicationLanguageCode))
			{
				languageCode = applicationLanguageCode.ToString();
			}
			else if (request.Headers.TryGetValue("Accept-Language", out var browserLanguageCode))
			{
				languageCode = browserLanguageCode.ToString().Split(",")[0].Split(";")[0];
			}
			if (string.IsNullOrWhiteSpace(languageCode))
			{
				languageCode = "fr-FR";
			}

			var isAuthenticated = request.Headers.ContainsKey(HeaderNames.Authorization);
			var jwt = isAuthenticated ? request.Headers[HeaderNames.Authorization].ToString().Substring(7) : null;

			Guid organisationId = Guid.Empty;

			Guid? organisationAdminId = null;
			Guid? organisationTypeId = null;
			Guid? userOrganisationId = null;
			Guid? roleId = null;

			// Coming from the claims -> common front
			var hasOrganisationId = request.Headers.TryGetValue("X-Organisation-Id", out var temp) && Guid.TryParse(temp, out organisationId);
			// Coming from the route  -> extension
			if (!hasOrganisationId)
			{
				hasOrganisationId = request.RouteValues.TryGetValue("organisationId", out var temp2) && Guid.TryParse(temp2.ToString(), out organisationId);
			}
			// Coming from the query -> signalr
			if (!hasOrganisationId)
			{
				hasOrganisationId = request.Query.TryGetValue("organisationId", out var temp3) && Guid.TryParse(temp3.ToString(), out organisationId);
			}

			if (hasOrganisationId)
			{
				var foundationClientFactory = context.RequestServices.GetRequiredService<IFoundationClientFactory>();
				var foundationClient = await foundationClientFactory.CreateAuthenticated(applicationId, languageCode, jwt);

				var organisation = await foundationClient.Gateway.Organisations.Get(organisationId);

				if (organisation == null)
				{
					throw new Exception(ErrorCode.MissingOrganisation);
				}

				organisationAdminId = organisation.AdminId;
				organisationTypeId = organisation.OrganisationTypeId;

				var userOrganisation = await foundationClient.Core.UserOrganisations.GetCurrent(organisationId);

				if (userOrganisation == null)
				{
					throw new Exception(ErrorCode.UserNotInOrganisation);
				}

				userOrganisationId = userOrganisation.Id;
				roleId = userOrganisation.RoleId;
			}

			provider.Context = new RequestContext()
			{
				ApplicationId = applicationId,
				ActorId = actorId,
				LanguageCode = languageCode,

				OrganisationId = hasOrganisationId ? organisationId : null,
				OrganisationAdminId = organisationAdminId,
				OrganisationTypeId = organisationTypeId,

				ActorOrganisationId = userOrganisationId,
				RoleId = roleId,
				Jwt = jwt
			};

			await _next(context);
		}
	}
}