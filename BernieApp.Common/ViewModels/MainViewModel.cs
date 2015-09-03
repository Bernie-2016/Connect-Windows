using BernieApp.Common.Http;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;

namespace BernieApp.Common.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _helloWorld = "Hello Wpf World";
        }

        private string _helloWorld;

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

        private async Task Test()
        {
            var newsClient = new NewsClient();
            var result = await newsClient.GetAsync();
        }
    }
}