using BernieApp.UWP.View;
using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Template10.Common;
using Template10.Services.NavigationService;
using Template10.Behaviors;

namespace BernieApp.UWP
{
    sealed partial class App : BootStrapper
    {
        public App()
        {
            this.InitializeComponent();
        }

        // runs even if restored from state
        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // content may already be shell when resuming
            if ((Window.Current.Content as Shell) == null)
            {
                // setup hamburger shell
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new Shell(nav);
            }
            return Task.CompletedTask;

            //var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
            //Window.Current.Content = new Shell(nav);
            //return Task.FromResult<object>(null);
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(NewsPage));
            return Task.FromResult<object>(null);
        }
    }

}
