using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using GalaSoft.MvvmLight.Command;

namespace BernieApp.WindowsPhone.ViewModels
{
    public class HubPageViewModel : MainViewModel
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;
        private FeedEntry _selectedItem;
        private RelayCommand _loadCommand;

        public HubPageViewModel(IBernieClient client)
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
        public void GoToDetailsPage() =>
            NavigationService.Navigate(typeof(View.NewsDetailPage), SelectedItem.Id.ToString());



    }
}
}
