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
using Windows.UI.Xaml.Navigation;
using Template10.Services.NavigationService;

namespace BernieApp.UWP.ViewModels
{
    public class ActionsViewModel : MainViewModel
    {
        private readonly ObservableCollection<ActionAlert> _alerts = new ObservableCollection<ActionAlert>();
        private readonly IBernieClient _client;
        private ActionAlert _selectedItem;
        private RelayCommand _loadCommand;

        public ActionsViewModel(IBernieClient client)
        {
            _client = client;
            GetActionsAsync();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            return base.OnNavigatingFromAsync(args);
        }

        private async Task GetActionsAsync()
        {
            var alerts = await _client.GetActionsAsync();
            _alerts.AddRange(alerts);
        }

        public ObservableCollection<ActionAlert> Alerts => _alerts;

        public ActionAlert SelectedItem
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
                        var alerts = await _client.GetActionsAsync();
                        _alerts.Clear();
                        _alerts.AddRange(alerts);
                    });
                }
                return _loadCommand;
            }

        }
    }
}
