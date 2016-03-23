using BernieApp.WindowsPhone.Common;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Hub Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace BernieApp.WindowsPhone.View
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private ListView _NewsFeedListView = default(ListView);

        public HubPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            //_NewsFeedListView.SelectedItem = null;

            //    Messenger.Default.Register<NotificationMessage<string>>(this, (message) =>
            //    {
            //        var m = message.Notification.ToString();
            //        if (m == "Reset")
            //        {       
            //            ListView newsfeed = FindChildControl<ListView>(NewsSection, "NewsFeedListView") as ListView;
            //            newsfeed.SelectedItem = null;
            //        }
            //    });
        }

        void HubPage_Loaded(object sender, RoutedEventArgs e)
        {
            this._NewsFeedListView.SelectedItem = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Activate(e.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Deactivate(e.Parameter);
        }

        private void Hub_SectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            if(Hub.SectionsInView[0] == NewsSection)
            {
                RefreshButton.Visibility = Visibility.Visible;
                AppBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
            }
            else
            {
                RefreshButton.Visibility = Visibility.Collapsed;
                AppBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
            }
        }

        private void newsButton_Click(object sender, RoutedEventArgs e)
        {
            Hub.ScrollToSection(NewsSection);
        }

        private void NewsFeedListView_Loaded(object sender, RoutedEventArgs e)
        {
            _NewsFeedListView = sender as ListView;
        }
    }
}
