using Xamarin.Forms;

namespace RSS.Views
{
	public partial class NewsTech
	{
		NewsTechViewModel viewModel;

		public NewsTech()
		{
			InitializeComponent();
			BindingContext = viewModel = new NewsTechViewModel();
		}
		
		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel?.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			viewModel?.OnDisappearing();
		}

		void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
	}
}