using BernieApp.WindowsPhone.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BernieApp.WindowsPhone.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = (string)e.Parameter;

            if (parameter == "Settings")
            {
                Pivot.SelectedIndex = 0;
            }
            else if (parameter == "About")
            {
                Pivot.SelectedIndex = 1;
            }
            else if (parameter == "Feedback")
            {
                Pivot.SelectedIndex = 2;
            }
            else
            {
                
            }

            base.OnNavigatedTo(e);
        }
    }
}
