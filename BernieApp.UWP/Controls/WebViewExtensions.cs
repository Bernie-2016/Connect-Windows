using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BernieApp.UWP.Controls
{
    public class WebViewExtensions
    {
        //Bind to the Uri 
        public static string GetUriSource(WebView view)
        {
            return (string)view.GetValue(UriSourceProperty);
        }

        public static void SetUriSource(WebView view, string value)
        {
            view.SetValue(UriSourceProperty, value);
        }

        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.RegisterAttached(
            "UriSource", typeof(string), typeof(WebViewExtensions),
            new PropertyMetadata(null, OnUriSourcePropertyChanged));

        private static void OnUriSourcePropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var webView = sender as WebView;
            if (webView == null)
                throw new NotSupportedException();

            if (e.NewValue != null)
            {
                var uri = new Uri(e.NewValue.ToString());
                webView.Navigate(uri);
            }
        }


        // Bind to the html string
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached(
               "HtmlString", typeof(string), typeof(WebViewExtensions), 
               new PropertyMetadata(null, OnHtmlStringChanged));

        // Getter and Setter
        public static string GetHtmlString(WebView obj)
        {
            return (string)obj.GetValue(HtmlStringProperty);
        }

        public static void SetHtmlString(WebView obj, string value)
        {
            obj.SetValue(HtmlStringProperty, value);
        }

        // Handler for property changes in the DataContext : set the WebView
        private static void OnHtmlStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var webView = sender as WebView;
            if (webView != null)
            {
                webView.NavigateToString((string)e.NewValue);
            }
        }
    }
}
