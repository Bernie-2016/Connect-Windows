using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Utils;

namespace BernieApp.UWP.ViewModels {
    public class NewsViewModel
    {
        private readonly ObservableCollection<NewsArticle> _items = new ObservableCollection<NewsArticle>();
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

        public ObservableCollection<NewsArticle> Items => _items;
    }
}
