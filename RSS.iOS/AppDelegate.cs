using FFImageLoading.Svg.Forms;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace RSS.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Xamarin.Calabash.Start();
			Forms.Init();
			FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
			var ignore = typeof(SvgCachedImage);
			FormsMaterial.Init();
			AnimationViewRenderer.Init();
			LoadApplication(new App());
			UITabBar.Appearance.TintColor = ((Color)Xamarin.Forms.Application.Current.Resources["PrimaryColor"]).ToUIColor();
			return base.FinishedLaunching(app, options);
		}
	}
}