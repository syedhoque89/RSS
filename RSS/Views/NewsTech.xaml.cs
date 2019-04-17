using Xamarin.Forms;

namespace RSS.Views
{
	public partial class NewsTech : ContentPage
	{
		NewsTechViewModel viewModel;

		public NewsTech()
		{
			InitializeComponent();
			BindingContext = viewModel = new NewsTechViewModel();
		}
	}
}