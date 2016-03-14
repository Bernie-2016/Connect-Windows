using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;

namespace BernieApp.UWP.ViewModels
{
    public class NewsViewModel : MainViewModel
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;
        private FeedEntry _selectedItem;
        private RelayCommand _loadCommand;

        public NewsViewModel(IBernieClient client)
        {
            _client = client;
            PopulateAsync();
        }

        private async Task PopulateAsync()
        {
            var news = await _client.GetNewsAsync();
            _items.AddRange(news);
            //TODO: Need to check for null values and handle non 200 codes
        }

        public ObservableCollection<FeedEntry> Items => _items;

        public FeedEntry SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }

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
        
        //Navigate to the NewsDetails page to view full article
        public void GoToDetailsPage()
        {
            var entry = SelectedItem;
            Messenger.Default.Send(entry);
            NavigationService.Navigate(typeof(NewsDetail));
        }
            



    }
}
