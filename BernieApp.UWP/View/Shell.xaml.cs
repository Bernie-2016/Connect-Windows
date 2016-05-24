using BernieApp.UWP.Messages;
using GalaSoft.MvvmLight.Messaging;
using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        }

        public Shell(INavigationService navigationService)
        {
            Instance = this;
            InitializeComponent();
            SetNavigationService(navigationService);

            Messenger.Default.Register<WelcomeMessage>(this, OnWelcomeMessageReceived);
        }

        private void OnWelcomeMessageReceived(WelcomeMessage msg)
        {
            this.Menu.HamburgerButtonVisibility = msg.WelcomeMessageType == WelcomeMessageType.Initial ? Visibility.Collapsed : Visibility.Visible;
            //Workaround to make Shell buttons stay hidden while the modal is showing when on a desktop and windows width is wide.
            switch (msg.WelcomeMessageType)
            {
                case WelcomeMessageType.Initial:
                    this.Menu.VisualStateNormalMinWidth = 6000;
                    break;
                case WelcomeMessageType.Accept:
                    this.Menu.VisualStateNormalMinWidth = 600;
                    break;              
            }
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            Menu.NavigationService = navigationService;
        }
    }
}
