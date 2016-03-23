using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BernieApp.WindowsPhone.View;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.WindowsPhone.Common;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace BernieApp.WindowsPhone.ViewModels
{
    public class HubPageViewModel : MainViewModel, INavigable
    {
        private readonly ObservableCollection<FeedEntry> _items = new ObservableCollection<FeedEntry>();
        private readonly IBernieClient _client;
        private readonly INavigationService _navigationService;
        private FeedEntry _selectedItem;
        private RelayCommand _loadCommand;
        private RelayCommand _settingsCommand;
        private RelayCommand _aboutCommand;
        private RelayCommand _feedbackCommand;

        public HubPageViewModel(IBernieClient client, INavigationService navigationService)
        {
            _client = client;
            _navigationService = navigationService;
            PopulateAsync();
        }

        public override void Activate(object parameter)
        {
            //Messenger.Default.Send(new NotificationMessage<string>("Reset_Listview", "Reset"));
        }

        public override void Deactivate(object parameter)
        {

        }

        private async Task PopulateAsync()
        {
            var news = await _client.GetNewsAsync();
            foreach (FeedEntry item in news)
            {
                _items.Add(item);
            }
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

        //Refresh the news feed
        public RelayCommand GotToSettingsPageCommand
        {
            get
            {
                if (_settingsCommand == null)
                {
                    _settingsCommand = new RelayCommand( () =>
                    {
                        _navigationService.NavigateTo(ViewModelLocator.SettingsPageKey, "Settings");
                    });
                }
                return _settingsCommand;
            }
        }

        public RelayCommand GotToAboutSectionCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo(ViewModelLocator.SettingsPageKey, "About");
                    });
                }
                return _aboutCommand;
            }
        }

        public RelayCommand GotToFeedbackSectionCommand
        {
            get
            {
                if (_feedbackCommand == null)
                {
                    _feedbackCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo(ViewModelLocator.SettingsPageKey, "Feedback");
                    });
                }
                return _feedbackCommand;
            }
        }

        //Navigate to the NewsDetails page to view full article
        public void GoToDetailsPage()
        {
            if (SelectedItem != null)
            {
                var entry = SelectedItem;
                _navigationService.NavigateTo(ViewModelLocator.NewsDetailPageKey, entry);
            }
        }

        //Navigate to Actions page
        public void GoToActionsPage()
        {
            _navigationService.NavigateTo(ViewModelLocator.ActionsPageKey);
        }
    }
}

