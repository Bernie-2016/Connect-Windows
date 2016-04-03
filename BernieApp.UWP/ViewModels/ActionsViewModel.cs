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
        private string _webViewSource;

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

        public string WebViewSource
        {
            get { return _webViewSource; }
            set { Set(ref _webViewSource, value); }
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

        public void FlipViewSelectionChanged()
        {
            if (SelectedItem != null)
            {
                string _bodyHTML = SelectedItem.BodyHTML;
                string _width = "100";
                string _videoWidth = "150";
                SetWebView(_bodyHTML, _width, _videoWidth);
            }
        }
        
        //Format the tweet/facebook embed for display in WebView control
        public void SetWebView(string bodyHTML, string width, string videowidth)
        {
            //pull from selectedItem, use SelectionChanged event on FlipView
            string html = String.Format("<html><head><style type='text/css'>html { height: 100%; width: 100%; border-radius: 4px; overflow-x: hidden; } body { margin: 0px; overflow-x: hidden; font-family: -apple-system, 'Helvetica Neue', 'Lucida Grande'; } body > blockquote, .custom-body { background-color: white; border-radius: 4px; display: inline-block; width: {0}px; padding: 8px; overflow: hidden; } .fb_iframe_widget iframe { margin-top: -10px !important; overflow-x: hidden;} .fb-post iframe { margin-left: -10px; } iframe[src^='https://www.youtube.com'] { width: {1}px !important; border-radius: 4px; overflow: hidden; }</style></head><body>{2}</body></html>",
                width, videowidth, bodyHTML);
            _webViewSource = html;
        }
    }
}
