using BernieApp.Portable.Models;
using BernieApp.UWP.Messages;
using BernieApp.UWP.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
                if(message.Id == Alert.Id && string.IsNullOrEmpty(message.Path))
                {
                    webView.Visibility = Visibility.Collapsed;
                    ProgressRing.Visibility = Visibility.Visible;
                }
            });

            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as ActionsViewModel;
            };
        }

        public ActionsViewModel ViewModel { get; set; }

        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var platformFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
            Debug.WriteLine(platformFamily);
            if (platformFamily == "Windows.Mobile")
            {
                webView.Width = e.NewSize.Width;
                webView.Height = e.NewSize.Height;
            }
            else
            {
                webView.Width = 552;
                webView.Height = 650;
            }
            
        }

        private void webView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            webView.Visibility = Visibility.Visible;
            ProgressRing.Visibility = Visibility.Collapsed;
        }
    }
}
