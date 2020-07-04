using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string CurrentCondition = value as string;
            var AMPM = getAMPM();
            if (CurrentCondition.Equals("Rain")) return "Resources/background_rain2.jpg";
            else if (CurrentCondition.Equals("Clear") && AMPM.Equals("AM")) return "/Resources/background_clear.jpg";
            else if (CurrentCondition.Equals("Clear") && AMPM.Equals("PM")) return "/Resources/background_night_clear.png";
            else if (CurrentCondition.Equals("Clouds") && AMPM.Equals("AM")) return "/Resources/background_clouds2.jpg";
            else if (CurrentCondition.Equals("Clouds") && AMPM.Equals("PM")) return "/Resources/background_night_clouds.jpg";
            else if (CurrentCondition.Equals("Smoke")) return "/Resources/background_clouds.jpg";
            else if (CurrentCondition.Equals("Mist")) return "/Resources/background_mist.jpg";
            return "/Resources/background_night_clear.png";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "/Resources/background_night_clear.png";
        }
        private string getTime()
        {
            return System.DateTime.Now.ToString("hh");
        }
        private string getAMPM()
        {
            var time = Int32.Parse(getTime());
            var AMPM = System.DateTime.Now.ToString("tt");
            string ret = null;
            if ((AMPM.Equals("오후") && (6 <= time && time <= 12)) || (AMPM.Equals("오전") && (time == 12 || (1 <= time && time <= 6)))) ret = "PM";
            else ret = "AM";
            return ret;
        }
    }
}
