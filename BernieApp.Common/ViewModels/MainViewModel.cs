using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace BernieApp.Common.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _helloWorld;
        private INavigationService _navSvc;

        public MainViewModel(INavigationService navSvc)
        {
            _navSvc = navSvc;
            _helloWorld = "Hello Wpf World";

            _navSvc.NavigateTo("Bernie", 2016);
        }

        public string HelloWorld
        {
            get
            {
                return _helloWorld;
            }

            set
            {
                if (_helloWorld != value)
                {
                    _helloWorld = value;
                    RaisePropertyChanged(() => HelloWorld);
                }
            }
        }
    }
}