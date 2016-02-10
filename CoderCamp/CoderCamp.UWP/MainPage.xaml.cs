using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace CoderCamp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            ApplicationView.PreferredLaunchViewSize = new Size(480, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.InitializeComponent();

            LoadApplication(new CoderCamp.App());
        }
    }
}
