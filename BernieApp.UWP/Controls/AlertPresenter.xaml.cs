using BernieApp.Portable.Models;
using BernieApp.UWP.Messages;
using BernieApp.UWP.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BernieApp.UWP.Controls
{
    public sealed partial class AlertPresenter : UserControl
    {
        public ActionAlert Alert
        {
            get { return (ActionAlert)GetValue(AlertProperty); }
            set { SetValue(AlertProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Alert.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlertProperty =
            DependencyProperty.Register("Alert", typeof(ActionAlert), typeof(AlertPresenter), new PropertyMetadata(0));


        public AlertPresenter()
        {

            this.InitializeComponent();

            Messenger.Default.Register<AlertMessage>(this, (message) =>
            {
                if (message.Id == Alert.Id && !string.IsNullOrEmpty(message.Path))
                {
                    Uri url = webView.BuildLocalStreamUri("Alert", message.Path);
                    StreamUriResolver resolver = new StreamUriResolver();
                    webView.NavigateToLocalStreamUri(url, resolver);
                    Debug.WriteLine(Windows.Storage.ApplicationData.Current.TemporaryFolder.Path + "\\" + message.Path);
                }
            });

            Messenger.Default.Register<AlertMessage>(this, (message) =>
            {
                if (message.Id == Alert.Id && string.IsNullOrEmpty(message.Path))
                {
                    scrollViewer.Visibility = Visibility.Collapsed;
                    ProgressRing.Visibility = Visibility.Visible;
                }
                if (message.Id != Alert.Id)
                {
                    scrollViewer.Visibility = Visibility.Collapsed;
                }
            });

            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as ActionsViewModel;
            };
        }

        public ActionsViewModel ViewModel { get; set; }

        private void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                Debug.WriteLine("Failed: {0}", args.WebErrorStatus.ToString());
                return;
            }
            ProgressRing.Visibility = Visibility.Collapsed;
            scrollViewer.Visibility = Visibility.Visible;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            webView.Width = e.NewSize.Width;
            webView.Height = e.NewSize.Height + 100;
        }

        private void webView_PermissionRequested(WebView sender, WebViewPermissionRequestedEventArgs args)
        {
            Debug.WriteLine(args.PermissionRequest.PermissionType.ToString());

        }

        private void webView_UnsafeContentWarningDisplaying(WebView sender, object args)
        {
            Debug.WriteLine(args.ToString());
        }

        private void webView_UnviewableContentIdentified(WebView sender, WebViewUnviewableContentIdentifiedEventArgs args)
        {
            Debug.WriteLine(args.Uri.ToString());
        }
    }
}
