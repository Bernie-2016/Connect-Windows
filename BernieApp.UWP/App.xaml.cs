using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Template10.Common;
using BernieApp.UWP.View;
using System.Linq;
using Parse;
using System.Diagnostics;
using Windows.UI.Notifications;
using NotificationsExtensions.Toasts;
using Microsoft.QueryStringDotNET;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using Windows.UI.ViewManagement;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel;

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
            ParsePush.ParsePushNotificationReceived += ParsePush_OnNotificationReceived;
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

            //Push notification registration
            await ParsePush.SubscribeAsync("");
            await ParseInstallation.CurrentInstallation.SaveAsync();
            await ParseAnalytics.TrackAppOpenedAsync();

            //Handle ToastNotifications (Foreground activation)
            if (args is ToastNotificationActivatedEventArgs)
            {
                var toastActivationArgs = args as ToastNotificationActivatedEventArgs;

                //TODO: Handle future arguments to open a specific action or news article

                if (Window.Current.Content is ActionsPage)
                {
                    Debug.WriteLine("Already viewing ActionsPage");
                }
                else
                {
                    NavigationService.Navigate(typeof(ActionsPage));
                }
            }

            //Handle ToastNotifications (Background activation)
            //BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
            //BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
            //{
            //    Name = "ToastTask",
            //    TaskEntryPoint = "BernieApp.UWP.BackgroundTasks.NotificationActionBackgroundTask"
            //};

            //builder.SetTrigger(new ToastNotificationActionTrigger());
            //BackgroundTaskRegistration registration = builder.Register();

            //Set statusbar
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

            //Ensure the current window is active
            Window.Current.Activate();
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
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

        public static void ParsePush_OnNotificationReceived(object sender, ParsePushNotificationEventArgs args)
        {
            //Pull in json payload
            var payload = args.Payload;

            Debug.WriteLine("notification received!");
            Debug.WriteLine(payload);

            string title = "";
            string content = "";
            //string logo = "";

            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText() { Text = title },
                BodyTextLine1 = new ToastText() { Text = content }
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual
            };

            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddDays(2);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
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
    }

}
