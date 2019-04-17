using System.Net.Http;

namespace RSS.Services
{
	public class HttpClientService : IHttpClientFactory
	{
		public static HttpClient CreateHttpClient()
		{
			return new HttpClient();
		}

		public HttpClient Create() => CreateHttpClient();
	}

	public interface IHttpClientFactory
	{
		HttpClient Create();
	}
}