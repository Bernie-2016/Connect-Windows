using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Humanizer;

namespace BernieApp.UWP.Converters

//Api returns the date as 2016-01-15T03:00:29.553277+00:00
{
    public class DateToHumanReadableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var date = (DateTime)value;
            TimeSpan ts = date - DateTime.Now;
            var humanDate = ts.Humanize();
            string result = string.Format("{0} ago", humanDate);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return new NotImplementedException();
        }
    }
}
