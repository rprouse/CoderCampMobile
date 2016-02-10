using Akavache;
using CoderCamp.Views;

using Xamarin.Forms;

namespace CoderCamp
{
    public class App : Application
    {
        public const string XamarinInsightsKey = "2bf0f9618920763f065335f1f36ddc6282ca4061";

        public App()
        {
            BlobCache.ApplicationName = "CoderCamp";

            // The root page of your application
            MainPage = new NavigationPage(new EventListView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
