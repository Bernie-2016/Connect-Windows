using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using BernieApp.UWP.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Utils;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;

namespace BernieApp.UWP.ViewModels
{
    public class NewsViewModel : MainViewModel
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;
        private FeedEntry _selectedItem;
        private double _itemWidth;
        private bool _isModal;
        private RelayCommand _loadCommand;

        public NewsViewModel(IBernieClient client)
        {
            _client = client;
            PopulateAsync();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Messenger.Default.Send(new NotificationMessage<string>("Reset_Listview", "Reset"));

            Messenger.Default.Register<WidthMessage>(this, (message) =>
            {
                var m = message.Width;
                this.ItemWidth = m;
            });

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        private async Task PopulateAsync()
        {
            _isModal = true;
            var news = await _client.GetNewsAsync();
            _items.AddRange(news);
            _isModal = false;
            //TODO: Need to check for null values and handle non 200 codes
        }

        public ObservableCollection<FeedEntry> Items => _items;

        public FeedEntry SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }

        public double ItemWidth
        {
            get { return _itemWidth; }
            set { Set(ref _itemWidth, value); }
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
                        try
                        {
                            var news = await _client.GetNewsAsync();
                            _items.Clear();
                            _items.AddRange(news);
                        }
                        catch (Exception ex)
                        {
                            //Handle if newsfeed doesn't load
                            throw;
                        }
                    });
                }
                return _loadCommand;
            }            
        }
        
        //Navigate to the NewsDetails page to view full article
        public void GoToDetailsPage()
        {
            if (SelectedItem != null)
            {
                var entry = SelectedItem;
                Messenger.Default.Send(new NotificationMessage<FeedEntry>(entry, "Selected_Entry"));
                NavigationService.Navigate(typeof(NewsDetail));
            }
        } 
    }
}
