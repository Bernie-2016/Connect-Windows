using BernieApp.Common.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;

namespace BernieApp.Common.ViewModels
{
    public class NewsItemViewModel : ViewModelBase
    {
        private INavigationService _navSvc;
        private IBernieHttpClient _httpClient;
        private NewsItem _item;
        private string _id;
        private string _title;

        #region Constructor

        public NewsItemViewModel(INavigationService navSvc, IBernieHttpClient httpClient)
        {
            _navSvc = navSvc;
            _httpClient = httpClient;

            InitProps();
            InitCommands();
        }

        //private async Task<NewsItemViewModel> InitializeAsync()
        //{
        //    await LoadDataAsync();
        //    return this;
        //}

        //public static Task<NewsItemViewModel> CreateAsync(INavigationService navSvc, IBernieHttpClient httpClient)
        //{
        //    var vm = new NewsItemViewModel(navSvc, httpClient);
        //    return vm.InitializeAsync();
        //}

        #endregion

        public async Task LoadDataAsync(string id)
        {
            _id = id;

            var hit = await _httpClient.GetNewsArticleAsync(_id);
            var source = hit.Source;

            Item = new NewsItem
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

            RaisePropertyChanged(() => Item);
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
           // Items = new ObservableCollection<NewsItem>();
        }

        public string Title
        {
            //just for testing
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public NewsItem Item
        {
            get
            {
                return _item;
            }

            set
            {
                if (_item != value)
                {
                    _item = value;
                    RaisePropertyChanged(() => Item);
                }
            }
        }

        #endregion
    }
}
