    using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace HowWear.Model
{
    public class Main : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double temp;

        public double Temp
        {
            get { return temp; }
            set { temp = value; OnPropertyChanged(nameof(Temp)); }
        }

        private double humidity;

        public double Humidity
        {
            get { return humidity; }
            set { humidity = value; OnPropertyChanged(nameof(Humidity)); }
        }


    }

    public class Wind : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private double speed;

        public double Speed
        {
            get { return speed; }
            set { speed = value; OnPropertyChanged(nameof(Speed)); }
        }

    }

    public class Clouds : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private double all;

        public double All
        {
            get { return all; }
            set { all = value; OnPropertyChanged(nameof(All)); }
        }

    }

    public class Rain : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private double three_h;

        public double Three_h
        {
            get { return three_h; }
            set { three_h = value; OnPropertyChanged(nameof(Three_h)); }
        }

    }
    public class Weather : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string main;

        public string Main
        {
            get { return main; }
            set { main = value; OnPropertyChanged(nameof(Main)); }
        }

    }

    public class CurrentWeatherInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private Wind wind;

        public Wind Wind
        {
            get { return wind; }
            set 
            { 
                wind = value;
                OnPropertyChanged(nameof(Wind));
            }
        }

        private Main main;

        public Main Main
        {
            get { return main; }
            set 
            { 
                main = value;
                OnPropertyChanged(nameof(Main));
            }
        }

        private Clouds clouds;

        public Clouds Clouds
        {
            get { return clouds; }
            set 
            {
                clouds = value;
                OnPropertyChanged(nameof(Clouds));
            }
        }

        private IList<Weather> weather;

        public IList<Weather> Weather
        {
            get { return weather; }
            set { weather = value; OnPropertyChanged(nameof(Weather));}
        }

        private int feeling;

        public int Feeling
        {
            get { return feeling; }
            set { feeling = value; OnPropertyChanged(nameof(Feeling)); }
        }

        private string condition;

        public string Condition
        {
            get { return condition; }
            set { condition = value; OnPropertyChanged(nameof(Condition)); }
        }
    }
}
