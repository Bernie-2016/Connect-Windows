using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Template10.Controls;
using Template10.Services.NavigationService;

namespace BernieApp.UWP.View
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-SplitView
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu { get { return Instance.Menu; } }

        public Shell()
        {
            Instance = this;
            InitializeComponent();
            Loaded += Shell_Loaded;
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public Shell(INavigationService navigationService)
        {
            Instance = this;
            InitializeComponent();
            SetNavigationService(navigationService);
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            Menu.NavigationService = navigationService;
        }
    }
}
