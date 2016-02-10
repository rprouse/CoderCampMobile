using CoderCamp.ViewModels;

using Xamarin.Forms;

namespace CoderCamp.Views
{
    public partial class EventListView : ContentPage
    {
        EventListViewModel ViewModel =>
            BindingContext as EventListViewModel;

        public EventListView()
        {
            InitializeComponent();

            BindingContext = new EventListViewModel(this);

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                //this.Navigation.PushAsync(new EventDetailsView(listView.SelectedItem as Event));
                listView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null || ViewModel.IsBusy || ViewModel?.Events.Count > 0)
                return;

            ViewModel.LoadEventsCommand.Execute(null);
        }
    }
}
