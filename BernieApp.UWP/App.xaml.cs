using BernieApp.UWP.View;
using Microsoft.QueryStringDotNET;
using Newtonsoft.Json.Linq;
using NotificationsExtensions.Toasts;
using Parse;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Template10.Common;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Metadata;
using Windows.Networking.PushNotifications;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace BernieApp.UWP
{
    sealed partial class App : BootStrapper
    {

        private string APP_ID = "r1LFJIyrrqlqGpNtbax93vTeq4NSM1QVd3pwzT6O";
        private string APP_KEY = "yJV5Mdu3xCNS9Mrvts1CM0MZfzY8TPov0CwZAtXv";

        public App()
        {
            this.InitializeComponent();            
            ParseClient.Initialize(APP_ID, APP_KEY);
        }

        // runs even if restored from state
        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // content may already be shell when resuming
            if ((Window.Current.Content as Shell) == null)
            {
                // setup hamburger shell
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new Shell(nav);
            }

            SetStatusBar();

            //Push notification registration
            await ParsePush.SubscribeAsync("");
            await ParseInstallation.CurrentInstallation.SaveAsync();
            await ParseAnalytics.TrackAppOpenedAsync(args as LaunchActivatedEventArgs);

            //Ensure the current window is active
            Window.Current.Activate();
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            var e = args as ILaunchActivatedEventArgs;

            //Activation is by Toast
            if (e.Arguments.Contains("action"))
            {
                var json = e.Arguments.ToString();
                JObject push = JObject.Parse(json);
                string type = (string)push["action"];
                string id = (string)push["identifier"];

                if (type.Contains("openActionAlert"))
                {
                    this.NavigationService.Navigate(typeof(ActionsPage), id);
                }
                else if (type.Contains("openNewsArticle"))
                {
                    this.NavigationService.Navigate(typeof(NewsDetail), id);
                }
            }
            else
            {
                this.NavigationService.Navigate(typeof(NewsPage));
            }

            switch (DetermineStartCause(args))
            {
                case AdditionalKinds.Primary:
                    //uncomment to see welcome message again
                    //localSettings.Values.Remove("lastRunDate");
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    object value = localSettings.Values["lastRunDate"];

                    if (value == null)
                    {
                        NavigationService.Navigate(typeof(WelcomePage));
                    }
                    else
                    {
                        NavigationService.Navigate(typeof(NewsPage));
                    }

                    break;
                case AdditionalKinds.Toast:
                    var toastArgs = args as ToastNotificationActivatedEventArgs; //This may be depricated
                    NavigationService.Navigate(typeof(ActionsPage), toastArgs.Argument);
                    break;
                case AdditionalKinds.SecondaryTile:
                    break;
                case AdditionalKinds.Other:
                    NavigationService.Navigate(typeof(NewsPage));
                    break;
                case AdditionalKinds.JumpListItem:
                    break;
                default:
                    NavigationService.Navigate(typeof(NewsPage));
                    break;
            }


            return Task.FromResult<object>(null);
        }

        public override void OnResuming(object s, object e, AppExecutionState previousExecutionState)
        {
            base.OnResuming(s, e, previousExecutionState);
        }

        public override Task OnSuspendingAsync(object s, SuspendingEventArgs e, bool prelaunchActivated)
        {
            return base.OnSuspendingAsync(s, e, prelaunchActivated);
        }

        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        public void SetStatusBar()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    var background = GetSolidColorBrush("#FF147FD7").Color;
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = background;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }
    }
}
