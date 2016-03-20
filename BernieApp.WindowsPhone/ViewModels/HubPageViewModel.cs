using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BernieApp.WindowsPhone.View;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace BernieApp.WindowsPhone.ViewModels
{
    public class HubPageViewModel : MainViewModel
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;
        private INavigationService _navigationService;
        private FeedEntry _selectedItem;
        private RelayCommand _loadCommand;

        public HubPageViewModel(IBernieClient client, INavigationService navigationService)
        {
            _client = client;
            _navigationService = navigationService;
            PopulateAsync();
        }

        public  override void Activate(object parameter)
        {
            Messenger.Default.Send(new NotificationMessage<string>("Reset_Listview", "Reset"));
        }

        public override void Deactivate(object parameter)
        {

        }


        //public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        //{
        //    Messenger.Default.Send(new NotificationMessage<string>("Reset_Listview", "Reset"));

        //    return base.OnNavigatedToAsync(parameter, mode, state);
        //}

        private async Task PopulateAsync()
        {
            var news = await _client.GetNewsAsync();
            foreach (FeedEntry item in news)
            {
                _items.Add(item);
            }
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
                        foreach (FeedEntry item in news)
                        {
                            _items.Add(item);
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
                _navigationService.NavigateTo("NewsDetailPage");
            }

        }
    }
}

