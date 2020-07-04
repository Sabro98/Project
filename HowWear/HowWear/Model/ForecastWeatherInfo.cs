using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowWear.Model
{

    public class FMain
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class FWeather
    {
        public string Main { get; set; }
    }

    public class FClouds
    {
        public double All { get; set; }
    }

    public class FWind
    {
        public double Speed { get; set; }
    }

    public class FList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public FMain Main { get; set; }
        public IList<FWeather> Weather { get; set; }
        public FClouds Clouds { get; set; }
        public FWind Wind { get; set; }
        public string dt_txt { get; set; }

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; OnPropertyChanged(nameof(Time)); }
        }

        private int fFeeling;

        public int FFeeling
        {
            get { return fFeeling; }
            set { fFeeling = value; OnPropertyChanged(nameof(FFeeling)); }
        }

        private string clothesProp;

        public string ClothesProp
        {
            get { return clothesProp; }
            set { clothesProp = value; OnPropertyChanged(nameof(ClothesProp)); }
        }

    }

    public class ForecastWeatherInfo :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IList<FList> list { get; set; }

        private FList firstInfo;

        public FList FirstInfo
        {
            get { return firstInfo; }
            set { firstInfo = value; OnPropertyChanged(nameof(FirstInfo)); }
        }

        private FList secondInfo;

        public FList SecondInfo
        {
            get { return secondInfo; }
            set { secondInfo = value; OnPropertyChanged(nameof(SecondInfo)); }
        }

        private FList thirdIfno;

        public FList ThirdInfo

        {
            get { return thirdIfno; }
            set { thirdIfno = value; OnPropertyChanged(nameof(ThirdInfo)); }
        }
    } 
}
