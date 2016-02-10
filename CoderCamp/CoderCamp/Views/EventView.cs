using CoderCamp.Models;
using Plugin.Share;
using Xamarin.Forms;

namespace CoderCamp.Views
{
    public class EventView : ContentPage
    {
        public EventView(Event @event)
        {
            Title = "CoderCamp";

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10),
                BackgroundColor = Color.White,

                Children = {
                    new Label {
                        Text = @event.Name,
                        FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
                    },
                    new Label {
                        Text = @event.Date,
                        FontAttributes = FontAttributes.Italic
                    },
                    new WebView {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Source = new HtmlWebViewSource {
                            Html = @event.Description
                        }
                    }                    
                }
            };

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "ic_share.png",
                Text = "Share",
                Command = new Command(() => 
                    CrossShare.Current
                        .Share($"Are you going to {@event.Name}? {@event.Link}"))
            });
        }
    }
}
