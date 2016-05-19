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
using BernieApp.UWP.ViewModels;
using BernieApp.UWP.Messages;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BernieApp.UWP.Controls
{
    public sealed partial class NewsPresenter : UserControl
    {
        public NewsPresenter()
        {
            this.InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as NewsViewModel;
            };

            Messenger.Default.Register<NotificationMessage<string>>(this, (message) =>
            {
                var m = message.Notification.ToString();
                if (m == "Reset")
                {
                    Newsfeed.SelectedItem = null;
                }
            });
        }

        public NewsViewModel ViewModel { get; set; }

        private void Newsfeed_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsWrapGrid MyItemsPanel = (ItemsWrapGrid)Newsfeed.ItemsPanelRoot;
            var width = e.NewSize.Width / 2;
            MyItemsPanel.ItemWidth = width;
            Messenger.Default.Send(new WidthMessage() { Width = width });     
        }
    }
}
