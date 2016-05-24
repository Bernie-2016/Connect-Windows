using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BernieApp.Portable.Models;

namespace BernieApp.UWP.Converters
{
    public class NewsDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate NewsItemTemplate { get; set; }
        public DataTemplate NewsItemWithImageTemplate { get; set; }
        public DataTemplate NewsItemWithVideoTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dataItem = item as FeedEntry;

            if (dataItem.ImageUrl.Contains("http"))
            {
                return NewsItemWithImageTemplate;
            }
            //Logic for video template here
            else
            {
                return NewsItemTemplate;
            }
        }
    }
}
