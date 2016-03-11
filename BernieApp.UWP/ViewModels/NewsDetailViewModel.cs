using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace BernieApp.UWP.ViewModels
{
    public class NewsDetailViewModel : MainViewModel
    {
        private readonly FeedEntry _item = new FeedEntry();
        private readonly IBernieClient _client;
        private string _id;

        public FeedEntry Item => _item;
        public string Id => _id;

        public NewsDetailViewModel(IBernieClient client)
        {
            _client = client;
        }

        public async Task GetArticleAsync(string id)
        {
            var article = await _client.GetNewsArticleAsync(id);
            article.Id = _item.Id;
            article.Title = _item.Title;
            article.Body = _item.Body;
            article.Date = _item.Date;
            article.Url = _item.Url;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //hide the hamburger button, navigation should only go back to the NewsPage via the hardware back button or back button in page header
            var h = Shell.HamburgerMenu;
            h.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            h.IsOpen = false;

            if (parameter != null)
            {
                _id = (string)parameter;
                await GetArticleAsync(Id);
            }            
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            var h = Shell.HamburgerMenu;
            h.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Visible;           

            return Task.CompletedTask;
        }

    }
}
