using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace BernieApp.UWP.ViewModels
{
    public class MainViewModel : GalaSoft.MvvmLight.ViewModelBase, Template10.Services.NavigationService.INavigable
    {
        public Task OnNavigatedToAsync(object parameter, NavigationMode mode,
        IDictionary<string, object> state)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync(IDictionary<string, object> state,
            bool suspending)
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            return Task.CompletedTask;
        }

        [JsonIgnore]
        public IDispatcherWrapper Dispatcher { get; set; }

        [JsonIgnore]
        public INavigationService NavigationService { get; set; }

        [JsonIgnore]
        public IStateItems SessionState { get; set; }
    }
}
