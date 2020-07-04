using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class FWindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? speed = value as double?;
            string ret = null;

            if (speed != null)
            {
                if (speed < 4.0) ret = "미풍";
                else if (speed < 9.0) ret = "약풍";
                else if (speed < 14.0) ret = " 강풍";
                else if (speed >= 14.0) ret = "매우 강풍";
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
