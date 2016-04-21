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
using System.Text.RegularExpressions;

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
                Messenger.Default.Send(new NotificationMessage("navigating"));

                string _bodyHTML = SelectedItem.BodyHTML;
                string _id = SelectedItem.Id;

                SetWebView(_bodyHTML, _id);
            }
        }
        
        //Format the tweet/facebook embed for display in WebView control
        public async void SetWebView(string bodyHTML, string id)
        {
            string result = EditString(bodyHTML);

            string path = await WriteHTML(result, id);

            _webViewSource = path;
            Messenger.Default.Send<string>(_webViewSource);
            
        }

        public string EditString(string bodyHTML)
        {
            string _newlineReplacement = " ";

            string htmlDecoded = System.Net.WebUtility.HtmlDecode(bodyHTML);
            string removeNewline = Regex.Replace(htmlDecoded, @"\r\n?|\n", _newlineReplacement);
            string htmlPage = String.Format(@"<html><head>
                    <style type='text/css'>
                            html {{
                                height: 100%;
                                width: 100%;
                                border-radius: 4px;
                                overflow-x: hidden;
                                overflow-y: hidden;
                            }}

                            span {{
                                width: 400px !important;
                                height: 600px !important;
                            }}

                            iframe {{
                                width: 400px !important;
                                height: 600px !important;
                            }}

                            iframe[src^='https://www.youtube.com'] {{
                                width: 400px !important;
                                border-radius: 4px;
                                overflow: hidden;
                            }}
                        </style>
                    </head><body>{0}</body></html>",
                removeNewline);
            if (htmlPage.Contains("//platform.twitter.com/widgets.js"))
            {
                htmlPage = Regex.Replace(htmlPage, "//platform.twitter.com/widgets.js", "https://platform.twitter.com/widgets.js");
            }
            if (htmlPage.Contains("//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.3"))
            {
                htmlPage = Regex.Replace(htmlPage, "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.3", "https://connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.3");
            }
            return htmlPage;
        }

        public async Task<string> WriteHTML(string bodyHTML, string id)
        {
            Windows.Storage.StorageFolder storageFolder =
                 Windows.Storage.ApplicationData.Current.TemporaryFolder;
            Windows.Storage.StorageFile file =
                await storageFolder.CreateFileAsync(string.Format("alert-{0}.html", id),
                    Windows.Storage.CreationCollisionOption.OpenIfExists);

            await Windows.Storage.FileIO.WriteTextAsync(file, bodyHTML);

            return file.DisplayName;
        }
    }
}
