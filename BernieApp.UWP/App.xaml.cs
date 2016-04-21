using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Template10.Common;
using BernieApp.UWP.View;
using System.Linq;
using Parse;
using System.Diagnostics;

namespace BernieApp.UWP
{
    sealed partial class App : BootStrapper
    {

        private string APP_ID = "";
        private string APP_KEY = "";

        public App()
        {
            this.InitializeComponent();

            //ParseClient.Initialize(APP_ID, APP_KEY);

            //ParsePush.ParsePushNotificationReceived += ParsePush_OnNotificationReceived;
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
            //await ParsePush.SubscribeAsync("");

            //// setup custom titlebar
            //foreach (var resource in Application.Current.Resources
            //    .Where(x => x.Key.Equals(typeof(Template10.Controls.CustomTitleBar))))
            //{
            //    var control = new Template10.Controls.CustomTitleBar();
            //    control.Style = resource.Value as Style;
            //}

            //return Task.CompletedTask;
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            switch (DetermineStartCause(args))
            {
                case AdditionalKinds.Primary:
                    NavigationService.Navigate(typeof(NewsPage));
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

        //public static void ParsePush_OnNotificationReceived(object sender, ParsePushNotificationEventArgs args)
        //{
        //    //Pull in json payload
        //    Debug.WriteLine("notification received!");
        //}
    }

}
