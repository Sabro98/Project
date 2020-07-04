using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class FHumideConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double? humid = values[0] as double?;
            double? temp = values[1] as double?;
            string ret = null;
            if (humid != null && temp != null)
            {
                if (temp < 18)
                {
                    if (humid < 65) ret = "건조";
                    else if (65 <= humid && humid <= 75) ret = "적당";
                    else ret = "습함";
                }
                else if (temp < 21)
                {
                    if (humid < 55) ret = "건조";
                    else if (55 <= humid && humid <= 65) ret = "적당";
                    else ret = "습함";
                }
                else if (temp < 24)
                {
                    if (humid < 45) ret = "건조";
                    else if (45 <= humid && humid <= 55) ret = "적당";
                    else ret = "습함";
                }
                else if (temp >= 24)
                {
                    if (humid < 45) ret = "건조";
                    else if (45 <= humid && humid <= 55) ret = "적당";
                    else ret = "습함";
                }
            }
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
