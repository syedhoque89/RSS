using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RSS.Models
{
	public class News
	{
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Thumbnail { get; set; }
		public string Url { get; set; }
		public ICommand NavigateToUrlCommand { get; set; }
		public ICommand ShareCommand { get; set; }

		public News()
		{
			NavigateToUrlCommand = new Command<string>(async url => await OnNavigateCommand(url));
			ShareCommand = new Command<string>(async url => await OnShareCommand(url));
		}

		public async Task OnNavigateCommand(string uri)
		{
			await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
		}

		public async Task OnShareCommand(string uri)
		{
			await Share.RequestAsync(new ShareTextRequest
			{
				Uri = uri,
				Title = $"{Localization.Resources.ShareTitle}{Environment.NewLine}{uri}"
			});
		}
	}
}