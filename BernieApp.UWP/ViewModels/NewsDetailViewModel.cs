using System;
using BernieApp.Portable.Client;
using BernieApp.Portable.Models;
using BernieApp.UWP.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Utils;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Navigation;

namespace BernieApp.UWP.ViewModels
{
    public class NewsDetailViewModel : MainViewModel
    {
        private readonly FeedEntry _item = new FeedEntry();
        private readonly IBernieClient _client;

        public FeedEntry Item => _item;

        public async Task GetArticleAsync(string id)
        {
            var article = await _client.GetNewsArticleAsync(id);
            article.Id = _item.Id;
            article.Title = _item.Title;
            article.Body = _item.Body;
            article.Date = _item.Date;
            article.Url = _item.Url;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //hide the hamburger button, navigation should only go back to the NewsPage via the hardware back button or back button in page header
            var h = Shell.HamburgerMenu;
            h.HamburgerButtonVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            h.IsOpen = false;

            if (parameter != null)
            {
                string id = (string)parameter;
                GetArticleAsync(id);
            }
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
