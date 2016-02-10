using Akavache;
using CoderCamp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace CoderCamp.ViewModels
{
    public class EventListViewModel : BaseViewModel
    {
        const string FEED_URI = "http://www.codercamphamilton.com/rss.xml";

        Page _page;
        string _version = typeof(EventListViewModel).GetTypeInfo().Assembly.GetName().Version.ToString(3);

        public ObservableCollection<Event> Events { get; } 
            = new ObservableCollection<Event>();

        public Command LoadEventsCommand { get; }

        public EventListViewModel(Page page)
        {
            LoadEventsCommand = new Command(() => LoadEvents());
            _page = page;
        }

        void LoadEvents()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IObservable<string> observable = CrossConnectivity.Current.IsConnected ?
                BlobCache.UserAccount.GetAndFetchLatest(FEED_URI, async () => await DownloadRss()) :   // Fetch from cache, then Internet
                BlobCache.UserAccount.GetObject<string>(FEED_URI); // Not connected to the Internet, fetch from cache only

            observable.ObserveOn(SynchronizationContext.Current)
                    .Subscribe(async events => await LoadEventsFromRss(events), () => IsBusy = false);
        }

        async Task<string> DownloadRss()
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CoderCampMobile", _version));
                    return await http.GetStringAsync(FEED_URI);
                }
            }
            catch
            {
                await _page.DisplayAlert("Error", "Unable to load events.", "OK");
                return null;
            }
        }

        async Task LoadEventsFromRss(string rss)
        {
            if (string.IsNullOrWhiteSpace(rss))
                return; // We failed to download?

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
