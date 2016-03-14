using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using BernieApp.Portable.Models;
using BernieApp.Portable.Client;

namespace BernieApp.WindowsPhone.ViewModels
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

            if (parameter != null)
            {
                _id = (string)parameter;
                await GetArticleAsync(Id);
            }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            return Task.CompletedTask;
        }
    }
}
