using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Akavache;
using Autofac;
using Newtonsoft.Json;
using RSS.Bootstrapper;

namespace RSS.Services
{
	public class UKNewsService : IUKNewsService
	{
		IHttpClientFactory client { get; set; }
		const string url = "https://feeds.bbci.co.uk/news/uk/rss.xml​";

		public UKNewsService ()
		{
			Bootstrap.Container.InjectUnsetProperties(this);
		}

		public IObservable<IEnumerable<object>> Get()
		{
			var offset = DateTimeOffset.UtcNow.AddHours(5);

			var url = "api/status/weekend";
			return BlobCache.UserAccount.GetOrFetchObject(url, async () => await GetRemoteNews(url),
														  offset);
		}

		async Task<IEnumerable<Object>> GetRemoteNews(string url)
		{
			using (var response = await client.Create().GetAsync(url,
																 new CancellationToken()).ConfigureAwait(false))
			{
				response.EnsureSuccessStatusCode();

				var xml = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				try
				{
					var doc = new XmlDocument();
					doc.LoadXml(xml);
					var jsonText = JsonConvert.SerializeXmlNode(doc);
					return null;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
				}

				return null;
			}
		}
	}

	public interface  IUKNewsService
	{
		IObservable<IEnumerable<object>> Get();
	}
}