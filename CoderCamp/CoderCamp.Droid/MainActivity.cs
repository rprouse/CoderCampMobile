
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin;

namespace CoderCamp.Droid
{
    [Activity(
        Label = "CoderCamp", 
        Icon = "@drawable/icon", 
        Theme = "@android:style/Theme.Holo.Light.DarkActionBar",
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Insights.Initialize(App.XamarinInsightsKey, this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

