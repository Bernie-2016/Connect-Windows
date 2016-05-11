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
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BernieApp.UWP.View
{
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 560, Height = 300 });
            //this.NavigationCacheMode = NavigationCacheMode.Required;

            //Messenger.Default.Register<NotificationMessage<string>>(this, (message) => 
            //{
            //    var m = message.Notification.ToString();
            //    if (m == "Reset")
            //    {
            //        Newsfeed.SelectedItem = null;                  
            //    }
            //});
        }
    }
}
