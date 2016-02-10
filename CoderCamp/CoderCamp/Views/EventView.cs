using CoderCamp.Models;
using Plugin.Share;
using System.Threading.Tasks;
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
                BackgroundColor = Color.White,

                Children = {
                    new StackLayout {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(10, 20, 10, 40),
                    BackgroundColor = Color.FromRgb(0x5F, 0x00, 0x0B),
                    Children = {
                            new Label {
                                Text = @event.Name,
                                TextColor = Color.White,
                                FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
                            },
                            new Label {
                                Text = @event.Date,
                                TextColor = Color.White,
                                FontAttributes = FontAttributes.Italic
                            }
                        }
                    },
                    new WebView {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Source = new HtmlWebViewSource {
                            Html = @event.Description
                        }
                    },
                    new Button
                    {
                        Text = "Register",
                        TextColor = Color.White,
                        BackgroundColor = Color.FromRgb(0x2B, 0x8D, 0x11),
                        BorderColor = Color.FromRgb(0x2B, 0x8D, 0x11),
                        BorderWidth = 0,
                        BorderRadius = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Command = new Command(async () => await CrossShare.Current
                            .OpenBrowser("http://codercamp.eventbrite.ca/")
                        )
                    }
                }
            };

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "ic_share.png",
                Text = "Share",
                Command = new Command(async () => await ShareEvent(@event))
            });
        }

        async Task ShareEvent(Event @event)
        {
            if(Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
            {
                await CrossShare.Current
                    .Share($"Are you going to {@event.Name}? {@event.Link}", @event.Name);
            }
            else
            {
                await CrossShare.Current
                       .ShareLink(@event.Link, $"Are you going to {@event.Name}?", @event.Name);
            }
        }
    }
}
