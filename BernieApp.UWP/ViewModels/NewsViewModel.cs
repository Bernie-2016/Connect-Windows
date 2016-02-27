using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Utils;
using GalaSoft.MvvmLight.Command;

namespace BernieApp.UWP.ViewModels {
    public class NewsViewModel : MainViewModel
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;
        private RelayCommand _loadCommand;
        private RelayCommand _itemClicked;

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

        //Refresh the news feed
        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(async () =>
                    {
                        //Clears the list, then adds from the server. TODO: A way to add only new items would probably be better.
                        var news = await _client.GetNewsAsync();
                        _items.Clear();
                        _items.AddRange(news);
                    });
                }
                return _loadCommand;
            }
        }

        //Send selected news item to the detail page
        //public RelayCommand ItemClicked
        //{
        //    //get
        //    //{
        //    //    if (_itemClicked == null)
        //    //    {
        //    //        _itemClicked = new RelayCommand(async () =>
        //    //        {
        //    //            //Do something here.
        //    //        });
        //    //    }
        //    //}
            
        //}

    }
}
