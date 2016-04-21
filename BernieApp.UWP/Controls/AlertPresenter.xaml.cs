using BernieApp.UWP.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public AlertPresenter()
        {

            this.InitializeComponent();

            Messenger.Default.Register<string>(this, (message) =>
            {
                if (message.Contains("alert"))
                {
                    Uri url = webView.BuildLocalStreamUri("Alert", message);
                    StreamUriResolver resolver = new StreamUriResolver();
                    webView.NavigateToLocalStreamUri(url, resolver);
                    webView.Visibility = Visibility.Visible;
                }
            });

            Messenger.Default.Register<NotificationMessage>(this, (message) =>
            {
                if(message.Notification == "navigating")
                {
                    webView.Visibility = Visibility.Collapsed;
                }
            });

            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as ActionsViewModel;
            };
        }

        public ActionsViewModel ViewModel { get; set; }
    }
}
