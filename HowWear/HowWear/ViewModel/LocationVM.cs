using HowWear.Model;
using HowWear.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HowWear.ViewModel
{
    class LocationVM : INotifyPropertyChanged
    {
        public LocationInfo Locations { get; set; }
        public CityCheckCommand TestCommand { get; set; }
        public LocationChangeCommand LocationCommand { get; set; }
        private bool possibleLocation;

        public bool PossibleLocation
        {
            get { return possibleLocation; }
            set { possibleLocation = value; OnPropertyChanged(nameof(PossibleLocation)); }
        }
        private bool checkEnable;

        public bool CheckEnable
        {
            get { return checkEnable; }
            set { checkEnable = value; OnPropertyChanged(nameof(CheckEnable)); }
        }

        public LocationVM()
        {
            TestCommand = new CityCheckCommand(this);
            LocationCommand = new LocationChangeCommand(this);
            Locations = new LocationInfo();
            PossibleLocation = false;
            CheckEnable = true;
            using (StreamReader reader = new StreamReader("Text/location.txt"))
            {
                bool flag = false;
                string city = reader.ReadLine();
                for (int i = 0; i < Locations.CityList.Count; i++)
                {
                    if (city.Equals(Locations.CityList[i]))
                    {
                        Locations.InitCity[i] = true;
                        flag = true;
                        break;
                    }
                }
                if (!flag) Locations.InitCity[0] = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
