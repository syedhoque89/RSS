using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Lottie.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace RSS.Droid
{
	[Activity(Label = "RSS", 
		Icon = "@mipmap/icon", 
		Theme = "@style/MainTheme", 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Platform.Init(this, savedInstanceState);
			Forms.SetFlags("FastRenderers_Experimental");
			Forms.Init(this, savedInstanceState);
			CachedImageRenderer.Init(true);
			var ignore = typeof(SvgCachedImage);
			FormsMaterial.Init(this, savedInstanceState);
			AnimationViewRenderer.Init();
			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
														[GeneratedEnum] Permission[] grantResults)
		{
			Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}