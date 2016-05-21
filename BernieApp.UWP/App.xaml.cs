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

            RegisterBackgroundTasks();

            //Push notification registration
            await ParsePush.SubscribeAsync("");
            await ParseInstallation.CurrentInstallation.SaveAsync();
            await ParseAnalytics.TrackAppOpenedAsync(); //Not functioning since ILaunchActivatedEventArgs are hidden by Template10.
            ParsePush.PushNotificationReceived += OnNotificationReceived;

            //Ensure the current window is active
            Window.Current.Activate();
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            switch (DetermineStartCause(args))
            {
                case AdditionalKinds.Toast:
                    var toastActivationArgs = args as ToastNotificationActivatedEventArgs;
                    QueryString query = QueryString.Parse(toastActivationArgs.Argument);
                    switch (query["action"])
                    {
                        case "openActionAlert":
                            string actionId = query["identifier"];
                            this.NavigationService.Navigate(typeof(ActionsPage), actionId);
                            break;

                        case "openNewsArticle":
                            string newsId = query["identifier"];
                            this.NavigationService.Navigate(typeof(NewsDetail), newsId);
                            break;

                        default:
                            this.NavigationService.Navigate(typeof(NewsPage));
                            break;
                    }
                    break;
                case AdditionalKinds.SecondaryTile:
                case AdditionalKinds.JumpListItem:
                case AdditionalKinds.Primary:
                case AdditionalKinds.Other:
                    this.NavigationService.Navigate(typeof(NewsPage));
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

        public static void OnNotificationReceived(object sender, PushNotificationReceivedEventArgs args)
        {
            //Pull in json payload
            String notificationContent = String.Empty;

            switch (args.NotificationType)
            {
                case PushNotificationType.Badge:
                    notificationContent = args.BadgeNotification.Content.GetXml();
                    break;

                case PushNotificationType.Tile:
                    notificationContent = args.TileNotification.Content.GetXml();
                    break;

                case PushNotificationType.Toast:
                    notificationContent = args.ToastNotification.Content.InnerText;
                    GenerateToast(notificationContent);
                    break;

                case PushNotificationType.Raw:
                    notificationContent = args.RawNotification.Content;
                    break;
            }
            args.Cancel = true;
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

        public async void RegisterBackgroundTasks()
        {
            //Check if background task is already activated or not
            var taskRegistered = false;
            var exampleTaskName = "BackgroundToastActivation";
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == exampleTaskName)
                {
                    taskRegistered = true;
                    break;
                }
            }
            if (taskRegistered == false)
            {
                //Toast Activation
                BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
                BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
                {
                    Name = "BackgroundToastActivation",
                    TaskEntryPoint = "BackgroundTasks.NotificationActionBackgroundTask"
                };
                builder.SetTrigger(new ToastNotificationActionTrigger());
                BackgroundTaskRegistration registration = builder.Register();
            }

        }

        public static void GenerateToast(string notificationContent)
        {
            //parse json
            JObject json = JObject.Parse(notificationContent);

            string title = (string)json["aps"]["alert"];
            //string content = "";
            string type = (string)json["action"];
            string identifier = (string)json["identifier"];

            try
            {
                ToastVisual visual = new ToastVisual()
                {
                    TitleText = new ToastText() { Text = title }
                    //BodyTextLine1 = new ToastText() { Text = content }
                };

                ToastContent toastContent = new ToastContent()
                {
                    Visual = visual,
                    Launch = new QueryString()
                    {
                        { "action", type },
                        { "identifier", identifier }
                    }.ToString()
                };

                var toast = new ToastNotification(toastContent.GetXml());
                toast.ExpirationTime = DateTime.Now.AddDays(2);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
            catch (Exception ex)
            {
                //Notification Failed to send
                Debug.WriteLine(ex.Message);
            }
        } 
    }
}
