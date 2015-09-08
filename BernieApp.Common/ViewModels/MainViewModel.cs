using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using BernieApp.Common.Models;
using System;
using System.Threading.Tasks;

namespace BernieApp.Common.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navSvc;
        private IBernieHttpClient _httpClient;

        public MainViewModel(INavigationService navSvc, IBernieHttpClient httpClient)
        {
            _navSvc = navSvc;
            _httpClient = httpClient;

            InitCommands();
            InitGroups();
        }

        public async Task InitNewsAsync()
        {
            News = await NewsViewModel.CreateAsync(_navSvc, _httpClient);
            RaisePropertyChanged(() => News);
        }

        private void InitGroups()
        {
            Groups = new List<MenuItem>();
            Groups.Add(new MenuItem
            {
                Title = "Feel"
            });
            Groups.Add(new MenuItem
            {
                Title = "The"
            });
            Groups.Add(new MenuItem
            {
                Title = "Bern"
            });
        }

        private void InitCommands()
        {
        }

        public List<MenuItem> Groups { get; set; }

        public NewsViewModel News { get; set; }
    }
}