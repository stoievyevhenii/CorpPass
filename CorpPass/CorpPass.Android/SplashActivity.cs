using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace CorpPass.Droid
{
    [Activity(Label = "CorpPass", MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(38, 38, 38));
            Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(38, 38, 38));
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }
        async void SimulateStartup()
        {
            await Task.Delay(4000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}