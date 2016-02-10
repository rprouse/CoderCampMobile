using CoderCamp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace CoderCamp.ViewModels
{
    public class EventListViewModel : BaseViewModel
    {
        const string FEED_URI = "http://www.codercamphamilton.com/rss.xml";

        Page _page;

        public ObservableCollection<Event> Events { get; } 
            = new ObservableCollection<Event>();

        public Command LoadEventsCommand { get; }

        public EventListViewModel(Page page)
        {
            LoadEventsCommand = new Command(async () => await LoadEventsAsync());

            _page = page;
        }

        async Task LoadEventsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                using (var http = new HttpClient())
                {
                    var rss = await http.GetStringAsync(FEED_URI);
                    await LoadEventsFromRss(rss);
                }
            }
            catch
            {
                await _page.DisplayAlert("Error", "Unable to load events.", "OK");
            }
            IsBusy = false;
        }

        async Task LoadEventsFromRss(string rss)
        {
            var events = await ParseRss(rss);
            Events.Clear();
            foreach (var @event in events)
            {
                Events.Add(@event);
            }
        }

        async Task<IEnumerable<Event>> ParseRss(string rss) =>
            await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var id = 0;
                return (from item in xdoc.Descendants("item")
                        select new Event
                        {
                            Name = (string)item.Element("title"),
                            Description = (string)item.Element("description"),
                            Link = (string)item.Element("link"),
                            Date = (string)item.Element("pubDate"),
                            Id = id++
                        }).ToList();
            });
    }
}
