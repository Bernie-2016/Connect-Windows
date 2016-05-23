using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Template10.Utils;
using Windows.System.Profile;
using Windows.UI.Xaml.Navigation;

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
            //TODO: handle navigated article ID here when activating from a toast, then move to correct flipview.

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            return base.OnNavigatingFromAsync(args);
        }

        private async void GetActionsAsync()
        {
            var alerts = await _client.GetActionsAsync();
            //workaround - as mobile webview control does not show fb-video posts correctly
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                alerts = alerts.Where(a => !a.BodyHTML.Contains("fb-video")).ToList();
            _alerts.Clear();
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
                    _loadCommand = new RelayCommand(GetActionsAsync);
                }
                return _loadCommand;
            }
        }

        public void FlipViewSelectionChanged()
        {
            if (SelectedItem != null)
            {
                Messenger.Default.Send(new AlertMessage(){ Id = SelectedItem.Id, Path = string.Empty });

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
            Messenger.Default.Send<AlertMessage>(new AlertMessage() { Id = id, Path = path });
            
        }

        public string EditString(string bodyHTML)
        {
            string _newlineReplacement = " ";

            string htmlDecoded = System.Net.WebUtility.HtmlDecode(bodyHTML);
            string removeNewline = Regex.Replace(htmlDecoded, @"\r\n?|\n", _newlineReplacement);

            string htmlPage = String.Format(@"<html><head>
                        <meta name=viewport content='width=device-width, height=device-height, initial-scale=1' />
                        <style type='text/css'>

                            html {{
                                width: {0};
                                height: {1};
                                border-radius: 4px;
                                overflow-x: hidden;
                                overflow-y: hidden;
                            }}

                            span {{
                                width: {0} !important;
                                height: {1} !important;
                                max-width: {3} !important;
                            }}

                            iframe {{
                                width: {0} !important;
                                height: {1} !important;
                                max-width: {3} !important;
                            }}

                            iframe[src^='https://www.youtube.com'] {{
                                width: {0} !important;
                                max-width: {3} !important;
                                border-radius: 4px;
                                overflow: hidden;
                            }}

                            .fb_iframe_widget {{
                                display: block !important;
                            }}
                            
                            .instagram-media {{
                                max-width: {3} !important;
                            }}

                        </style>
                    </head><body>{2}</body></html>",
                "100%", "100%", removeNewline, "100%");
            if (htmlPage.Contains("//platform.twitter.com/widgets.js"))
            {
                htmlPage = Regex.Replace(htmlPage, "//platform.twitter.com/widgets.js", "https://platform.twitter.com/widgets.js");
            }
            if (htmlPage.Contains("//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.3"))
            {
                htmlPage = Regex.Replace(htmlPage, "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.3", "https://connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.3");
            }
            if (htmlPage.Contains("//platform.instagram.com/en_US/embeds.js"))
            {
                htmlPage = Regex.Replace(htmlPage, "//platform.instagram.com/en_US/embeds.js", "https://platform.instagram.com/en_US/embeds.js");
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
