using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using Autofac;
using MvvmHelpers;
using RSS.Bootstrapper;
using RSS.Dto;
using RSS.Services;
using Xamarin.Forms;

namespace RSS.Views
{
	public class NewsUKViewModel : BaseViewModel
	{
		public ObservableRangeCollection<NewsItem> NewsItems { get; set; }
		public IUKNewsService UkNewsService { get; set; }
		public ICommand LoadItemsCommand { get; set; }
		public ICommand RefreshCommand { get; set; }
		IDisposable newsServiceSubscription;

		public NewsUKViewModel()
		{
			Bootstrap.Container.InjectUnsetProperties(this);
			NewsItems = new ObservableRangeCollection<NewsItem>();
			LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
			RefreshCommand = new Command(ExecuteLoadItemsCommand);
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

			newsServiceSubscription = UkNewsService.Get("https://feeds.bbci.co.uk/news/uk/rss.xml")
												   .ObserveOn(SynchronizationContext.Current)
												   .Subscribe(OnNext, OnError, OnComplete);

			void OnNext(IEnumerable<NewsItem> items)
			{
				var newsItems = items.ToList();
				if (newsItems.Any())
				{
					NewsItems.ReplaceRange(newsItems);
				}
			}

			void OnError(Exception ex)
			{
				IsBusy = false;
			}

			void OnComplete()
			{
				IsBusy = false;
			}
		}
	}
}