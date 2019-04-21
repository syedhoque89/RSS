using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace RSS.Droid
{
	[Activity(Theme = "@style/MainTheme.Splash",
			  MainLauncher = true,
			  Label = "RSS",
			  Icon = "@mipmap/ic_launcher",
			  ScreenOrientation = ScreenOrientation.Portrait,
			  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			  NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}
