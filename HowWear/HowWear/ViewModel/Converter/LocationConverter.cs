using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string location = value as string;
            switch (location)
            {
                case "Seoul": return "서울";
                case "Jeonju": return "전주";
                case "Daejeon": return "대전";
                case "Busan": return "부산";
                case "Daegu": return "대구";
                case "Gwangju": return "광주";
                default: return location;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {   
            return "";
        }
    }
}
