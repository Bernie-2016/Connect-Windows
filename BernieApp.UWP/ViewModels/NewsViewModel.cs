using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Utils;

namespace BernieApp.UWP.ViewModels {
    public class NewsViewModel : MainViewModel
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;

        public NewsViewModel(IBernieClient client)
        {
            _client = client;
            PopulateAsync();
        }

        private async Task PopulateAsync()
        {
            var news = await _client.GetNewsAsync();
            _items.AddRange(news);
        }

        public ObservableCollection<FeedEntry> Items => _items;
    }
}
