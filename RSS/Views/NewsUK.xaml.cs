namespace RSS.Views
{
	public partial class NewsUK
	{
		NewsUKViewModel viewModel;

		public NewsUK()
		{
			InitializeComponent();
			BindingContext = viewModel = new NewsUKViewModel();
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
	}
}