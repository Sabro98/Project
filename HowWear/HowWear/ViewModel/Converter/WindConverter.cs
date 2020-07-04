using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class WindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? speed = value as double?;
            string ret = null;
            
            if(speed != null)
            {
                if (speed < 4.0) ret = "바람 : 약함";
                else if (speed < 9.0) ret = "바람 : 약간 강함";
                else if (speed < 14.0) ret = "바람 : 강함";
                else if (speed >= 14.0) ret = "바람 : 매우 강함";
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
