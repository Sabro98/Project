using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class ComboBoxLocationConverter : IValueConverter
    {
        private List<Pair<string, string>> cities;
        public ComboBoxLocationConverter()
        {
            cities = new List<Pair<string, string>>
            {
                new Pair<string, string>("서울", "Seoul"),
                new Pair<string, string>("전주", "Jeonju"),
                new Pair<string, string>("대전", "Daejeon"),
                new Pair<string, string>("대구", "Daegu"),
                new Pair<string, string>("부산", "Busan"),
                new Pair<string, string>("광주", "Gwangju")
            };

        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (value as ComboBoxItem).ToString();
            if(str != null)
            {
                foreach(var tmp in cities)
                {
                    if (str.Contains(tmp.First)) return tmp.Second;
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
