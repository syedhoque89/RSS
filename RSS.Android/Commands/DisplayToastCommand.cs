using Android.App;
using Android.Widget;
using RSS.Commands;
using RSS.Droid.Commands;

[assembly: Xamarin.Forms.Dependency(typeof(DisplayToastCommand))]

namespace RSS.Droid.Commands
{
	public class DisplayToastCommand : IDisplayToastCommand
	{
		public void Execute(string message, bool longDuration = false)
		{
			if (longDuration)
				Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
			else
				Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
		}
	}
}