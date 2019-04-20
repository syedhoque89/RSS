using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using Autofac;
using MvvmHelpers;
using RSS.Bootstrapper;
using RSS.Dtos;
using RSS.Models;
using RSS.Services;
using Xamarin.Forms;

namespace RSS.Views
{
	public class NewsTechViewModel : BaseViewModel
	{
		public ObservableRangeCollection<News> NewsItems { get; set; }

		public INewsService NewsService { get; set; }
		public ICommand LoadItemsCommand { get; set; }
		public ICommand RefreshCommand { get; set; }
		public ICommand RetryCommand { get; set; }

		IDisposable newsServiceSubscription;

		public NewsTechViewModel()
		{
			Bootstrap.Container.InjectUnsetProperties(this);
			NewsItems = new ObservableRangeCollection<News>();
			LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
			RefreshCommand = new Command(ExecuteLoadItemsCommand);
			RetryCommand = new Command(ExecuteLoadItemsCommand);
		}

		public override void OnAppearing()
		{
			ExecuteLoadItemsCommand();
		}

		public override void OnDisappearing()
		{
			newsServiceSubscription?.Dispose();
		}

		void ExecuteLoadItemsCommand()
		{
			IsBusy = true;

			newsServiceSubscription = NewsService.Get("https://feeds.bbci.co.uk/news/technology/rss.xml")
												 .ObserveOn(SynchronizationContext.Current)
												 .Subscribe(OnNext, OnError, OnComplete);

			void OnNext(IEnumerable<NewsItem> items)
			{
				var newsItems = items.ToList();
				if (!newsItems.Any())
					return;

				//newsItems = items.OrderByDescending(i => i.PubDate).ToList();
				var news = new List<News>();

				foreach (var newsItem in newsItems)
				{
					news.Add(new News
					{
						Title = newsItem.Title.Text,
						Summary = newsItem.Description.Text,
						Thumbnail = newsItem.MediaThumbnail.Url,
						Url = newsItem.Link.AbsoluteUri
					});
				}

				NewsItems.ReplaceRange(news);
			}

			void OnError(Exception ex)
			{
				IsBusy = false;
				ErrorOccurred = true;
				Debug.WriteLine(ex);
			}

			void OnComplete()
			{
				IsBusy = false;

				ErrorOccurred &= !NewsItems.Any();
			}
		}
	}
}