using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;

namespace USOD.WebASP.Services.Implementations
{
	public class HttpHandler : DelegatingHandler
	{
		private readonly IMemoryCache _memoryCache;

		public HttpHandler(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if(_memoryCache.TryGetValue("Authorization", out var token))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
				return await base.SendAsync(request, cancellationToken);
			}
			return await base.SendAsync(request, cancellationToken);
		}

	}
}
