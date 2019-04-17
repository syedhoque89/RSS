using Xamarin.Forms;

namespace RSS.Views
{
	public partial class NewsUK : ContentPage
	{
		NewsUKViewModel viewModel;

		public NewsUK()
		{
			InitializeComponent();
			BindingContext = viewModel = new NewsUKViewModel();
		}
	}
}