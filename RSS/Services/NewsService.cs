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
using RSS.Dtos;

namespace RSS.Services
{
	public class NewsService : INewsService
	{
		public IHttpClientFactory Client { get; set; }

		public NewsService ()
		{
			Bootstrap.Container.InjectUnsetProperties(this);
		}

		public IObservable<IEnumerable<NewsItem>> Get(string url)
		{
			var offset = DateTimeOffset.UtcNow.AddHours(5);

			return BlobCache.UserAccount.GetOrFetchObject(url, async () => await GetRemoteNews(url),
														  offset);
		}

		public async Task<IEnumerable<NewsItem>> GetRemoteNews(string url)
		{
			using (var response = await Client.Create().GetAsync(url,
																 new CancellationToken()).ConfigureAwait(false))
			{
				response.EnsureSuccessStatusCode();

				var xml = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				try
				{
					var doc = new XmlDocument();
					doc.LoadXml(xml);

					var jsonString = JsonConvert.SerializeXmlNode(doc);

					return NewsDto.FromJson(jsonString).Rss.Channel.NewsItem;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
				}

				return new List<NewsItem>();
			}
		}
	}

	public interface  INewsService
	{
		IObservable<IEnumerable<NewsItem>> Get(string url);
	}
}