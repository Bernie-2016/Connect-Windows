using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Navigation;

namespace BernieApp.UWP.ViewModels
{
    public class NewsDetailViewModel : MainViewModel
    {
        private FeedEntry _item = new FeedEntry();
        //private readonly IBernieClient _client;

        public FeedEntry Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public NewsDetailViewModel(IBernieClient client)
        {
            //_client = client;

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
                    _item.ImageUrl = entry.ImageUrl;
                }

            });   
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //hide the hamburger button, navigation should only go back to the NewsPage via the hardware back button or back button in page header
            var h = Shell.HamburgerMenu;
            h.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            h.IsOpen = false;
 
            return Task.CompletedTask;        
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            var h = Shell.HamburgerMenu;
            h.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Visible;

            return Task.CompletedTask;
        }

    }
}
