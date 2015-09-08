using BernieApp.Common.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace BernieApp.Common.ViewModels
{
    public class NewsViewModel : ViewModelBase
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
            var hitNewsData = await _httpClient.GetNewsAsync();
            foreach(var hit in hitNewsData)
            {
                var source = hit.Source;
                var newsItem = new NewsItem
                {
                    Body = source.Body,
                    BodyHtml = source.BodyHtml,
                    Charset = source.Charset,
                    ContentLength = source.ContentLength,
                    Description = source.Description,
                    Id = hit.Id,
                    ImgUrl = source.ImgUrl,
                    Locale = source.Locale,
                    MimeType = source.MimeType,
                    PublishedTime = source.PublishedTime,
                    Title = source.Title,
                    Url = source.Url
                };
                Items.Add(newsItem);
            }

            RaisePropertyChanged(() => Items);
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
