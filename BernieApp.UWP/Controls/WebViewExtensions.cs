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


        public static string GetHTML(DependencyObject obj)
        {
            return (string)obj.GetValue(HTMLProperty);
        }

        public static void SetHTML(DependencyObject obj, string value)
        {
            obj.SetValue(HTMLProperty, value);
        }

        // Using a DependencyProperty as the backing store for HTML.  This enables animation, styling, binding, etc...  
        public static readonly DependencyProperty HTMLProperty =
            DependencyProperty.RegisterAttached(
                "HTML", typeof(string), typeof(WebViewExtensions), 
                new PropertyMetadata(0, new PropertyChangedCallback(OnHTMLChanged)));

        private static void OnHTMLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            WebView wv = d as WebView;
            if (wv != null)
            {
                wv.NavigateToString((string)e.NewValue);
            }
        }
    }
}
