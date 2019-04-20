using System.Threading.Tasks;
using PropertyChanged;

namespace RSS
{
	[AddINotifyPropertyChangedInterface]
	public abstract class BaseViewModel
	{
		public bool IsBusy { get; set; }
		public bool ErrorOccurred { get; set; }

		public abstract void OnAppearing();
		public abstract void OnDisappearing();
	}
}