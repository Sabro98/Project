using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class ConditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            switch (str)
            {
                case "Rain": return "비가 와요! 우산을 챙기세요!";
                case "Snow": return "눈이 와요! 우산을 챙기세요!";
                case "Clear": return "지금 날씨가 맑아요!";
                case "Clouds": return "지금은 하늘에 구름이 꼈어요!";
                case "Mist": return "지금은 밖이 습해요!";
                case "Haze" : 
                case "Smoke": return "지금은 안개가 껴있어요...";
                case "Drizzle": return "비가 조금씩 와요! 우산을 챙기세요!";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
