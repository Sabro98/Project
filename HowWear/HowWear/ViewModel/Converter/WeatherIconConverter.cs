using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HowWear.ViewModel.Converter
{
    class WeatherIconConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string CurrentCondition = values[0] as string;
            string timeTmp = values[1] as string;
            if(CurrentCondition != null && timeTmp != null)
            {
                timeTmp = timeTmp.Remove(timeTmp.Length - 1, 1);
                string AMPM = getAMPM(int.Parse(timeTmp));
                string path;
                if (CurrentCondition.Equals("Rain")) path = "/Resources/rainIcon.png";
                else if (CurrentCondition.Equals("Clear") && AMPM.Equals("AM")) path = "/Resources/sunIcon.png";
                else if (CurrentCondition.Equals("Clear") && AMPM.Equals("PM")) path = "/Resources/clearNightIcon.png";
                else if (CurrentCondition.Equals("Clouds") && AMPM.Equals("AM")) path = "/Resources/cloudIcon.png";
                else if (CurrentCondition.Equals("Clouds") && AMPM.Equals("PM")) path = "/Resources/cloudNightIcon.png";
                else if (CurrentCondition.Equals("Smoke")) path = "/Resources/mistIcon.png";
                else if (CurrentCondition.Equals("Mist")) path = "/Resources/mistIcon.png";
                else path = "/Resources/background_night_clear.png";
                return new BitmapImage(new Uri(path, UriKind.Relative));
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
        private string getAMPM(int time)
        {
            string ret = null;
            if (6 <= time && time < 18) ret = "AM";
            else ret = "PM";
            return ret;
        }
    }
}
