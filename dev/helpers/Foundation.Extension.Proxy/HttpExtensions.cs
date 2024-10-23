using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Yarp.ReverseProxy.Forwarder;
using System.Net.Http.Json;
using Microsoft.Net.Http.Headers;

namespace Foundation.Extension.Proxy.Extensions
{
	public static class HttpExtensions
	{
		static string NOT_FORWARDED_HEADERS = "Connection,Keep-Alive,Proxy-Authenticate,Proxy-Authorization,TE,Trailers,Transfer-Encoding,Upgrade,Host";

		public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, HttpContext context, string destinationPrefix, string overridePath = null)
		{
			var request = new HttpRequestMessage()
			{
				Method = HttpMethod.Get,
				Content = context.Request.Body != null ? new StreamContent(context.Request.Body) : null,
				RequestUri = new UriBuilder(new Uri(destinationPrefix).Scheme, new Uri(destinationPrefix).Host,
					context.Request.Host.Port ?? -1,
					overridePath ?? context.Request.PathBase.Value +
					context.Request.Path.Value,
					context.Request.QueryString.Value)
					.Uri
			};

			foreach (var header in context.Request.Headers.Where(h => !NOT_FORWARDED_HEADERS.Contains(h.Key)))
			{
				request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
			}

			var response = await client.SendAsync(request, context.RequestAborted);

			response.EnsureSuccessStatusCode();

			return response;
		}

		public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, HttpContext context, string destinationPrefix, string overridePath = null, T body = default)
		{
			var request = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				Content = body != null ? JsonContent.Create(body) : null,
				RequestUri = new UriBuilder(new Uri(destinationPrefix).Scheme, new Uri(destinationPrefix).Host,
					context.Request.Host.Port ?? -1,
					overridePath ?? context.Request.PathBase.Value +
					context.Request.Path.Value,
					context.Request.QueryString.Value)
					.Uri
			};

			foreach (var header in context.Request.Headers.Where(h => !NOT_FORWARDED_HEADERS.Contains(h.Key)))
			{
				request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
			}

			var response = await client.SendAsync(request, context.RequestAborted);

			response.EnsureSuccessStatusCode();

			return response;
		}


		public static async Task<List<JsonElement>> Concat(params HttpResponseMessage[] responses)
		{
			var result = new List<JsonElement>();

			foreach (var response in responses)
			{
				if (response != null && response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var array = JsonSerializer.Deserialize<List<JsonElement>>(content);
					result.AddRange(array);
				}
			}

			return result;
		}
	}
}