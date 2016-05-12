using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BernieApp.UWP.Converters
{
    public class ArticleTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            switch (value.ToString())
            {
                case "PressRelease":
                    return "PRESS RELEASE";
                case "DemocracyDaily":
                    return "DEMOCRACY DAILY";
                case "News":
                    return "NEWS";

                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return new NotImplementedException();
        }
    }
}
