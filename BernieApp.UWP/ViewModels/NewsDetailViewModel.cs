using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Popups;
using System.Diagnostics;
using GalaSoft.MvvmLight.Command;
using Windows.ApplicationModel.DataTransfer;

namespace BernieApp.UWP.ViewModels
{
    public class NewsDetailViewModel : MainViewModel
    {
        private readonly IBernieClient _client;
        private FeedEntry _item = new FeedEntry { ArticleType = ""}; //Instantiating an article type to prevent a xaml bug while trying to convert ArticleType before it's set by Messenger/OnNavigatedTo
        private RelayCommand _openWebPageCommand;
        private RelayCommand _shareCommand;

        public FeedEntry Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public NewsDetailViewModel(IBernieClient client)
        {
            _client = client;

            Messenger.Default.Register<NotificationMessage<FeedEntry>>(this, (message) =>
            {
                var entry = message.Content;
                if (message.Notification == "Selected_Entry")
                {
                    _item.Id = entry.Id;
                    _item.Title = entry.Title;
                    _item.ArticleType = entry.ArticleType;
                    _item.Date = entry.Date;
                    _item.Body = entry.Body;
                    _item.Url = entry.Url;
                    if (entry.ImageUrl == "")
                    {
                        _item.ImageUrl = "ms-appx-web:///Assets/bleh.png"; //ImageUrl can't be a non-url, so this allows nothing to display when there isn't an image.
                    }
                    else
                    {
                        _item.ImageUrl = entry.ImageUrl;
                    }
                }
            });
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //TODO: handle navigated article ID here when activating from a toast and retrieve from feedclient.
            if (parameter != null)
            {
                try
                {
                    var id = parameter.ToString();
                    var entry = await _client.GetNewsArticleAsync(id);

                    _item.Id = entry.Id;
                    _item.Title = entry.Title;
                    _item.ArticleType = entry.ArticleType;
                    _item.Date = entry.Date;
                    _item.Body = entry.Body;
                    _item.Url = entry.Url;
                    if (entry.ImageUrl == "")
                    {
                        _item.ImageUrl = "ms-appx-web:///Assets/bleh.png"; //ImageUrl can't be a non-url, so this allows nothing to display when there isn't an image.
                    }
                    else
                    {
                        _item.ImageUrl = entry.ImageUrl;
                    }
                }
                catch (Exception)
                {
                    //Something is wrong with the id OR there isn't an internet connection.
                    //If id issue, best to navigate back to NewsPage. If no internet, display some sort of indication with ability to refresh and try to get the article again.
                    throw;
                }
            }

            //Register for share
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
                  
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            //Un-register for share
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;

            //var h = Shell.HamburgerMenu;
            //h.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Visible;

            return Task.CompletedTask;
        }

        //Open article Url in Web Browser
        public RelayCommand OpenWebPageCommand
        {
            get
            {
                if (_openWebPageCommand == null)
                {
                    _openWebPageCommand = new RelayCommand(async () =>
                    {
                        var success = await Launcher.LaunchUriAsync(new Uri(Item.Url));
                        if (success)
                        {
                            Debug.WriteLine("url successfully opened in browser");
                        }
                        else
                        {
                            var dialog = new MessageDialog("Unable to open the webpage, please try again later.", "Oops...");
                            await dialog.ShowAsync();
                        }
                    });
                }
                return _openWebPageCommand;
            }

        }

        //Invoke Share charm to share a link to the article
        public RelayCommand ShareCommand
        {
            get
            {
                if (_shareCommand == null)
                {
                    _shareCommand = new RelayCommand( () =>
                    {
                       DataTransferManager.ShowShareUI();
                    });
                }
                return _shareCommand;
            }
        }

        void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;

            request.Data.Properties.Title = " ";
            request.Data.Properties.Description = "Share this news article";
            request.Data.SetWebLink(new Uri(Item.Url));
        }
    }
}
