using BernieApp.Common.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;

namespace BernieApp.Common.ViewModels
{
    public class NewsViewModel
    {
        private INavigationService _navSvc;
        private IBernieHttpClient _httpClient;

        #region Constructor

        private NewsViewModel(INavigationService navSvc, IBernieHttpClient httpClient)
        {
            _navSvc = navSvc;
            _httpClient = httpClient;

            InitProps();
            InitCommands();
        }

        private async Task<NewsViewModel> InitializeAsync()
        {
            await LoadDataAsync();
            return this;
        }

        public static Task<NewsViewModel> CreateAsync(INavigationService navSvc, IBernieHttpClient httpClient)
        {
            var vm = new NewsViewModel(navSvc, httpClient);
            return vm.InitializeAsync();
        }

        #endregion

        private async Task LoadDataAsync()
        {
            Items.Clear();
            var newsArticles = await _httpClient.GetNewsAsync();
            foreach(var article in newsArticles)
            {
                var newsItem = new NewsItem
                {
                    Body = article.Body,
                    BodyHtml = article.BodyHtml,
                    Charset = article.Charset,
                    ContentLength = article.ContentLength,
                    Description = article.Description
                };
                Items.Add(newsItem);
            }
        }

        #region Commands

        private void InitCommands()
        {
            OpenNewsCmd = new RelayCommand<NewsItem>((args) =>
            {
                _navSvc.NavigateTo("News", args.Id);
            });
        }

        public RelayCommand<NewsItem> OpenNewsCmd { get; private set; }

        #endregion

        #region Properties

        private void InitProps()
        {
            Items = new ObservableCollection<NewsItem>();
        }

        public ObservableCollection<NewsItem> Items { get; set; }

        #endregion
    }
}
