using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowWear.Model
{
    class LocationInfo
    {
        public ObservableCollection<string> CityList { get; set; }
        public List<bool> InitCity { get; set; }
        public LocationInfo()
        {
            CityList = new ObservableCollection<string>
            {
                "Seoul",
                "Jeonju",
                "Daejeon",
                "Busan",
                "Daegu",
                "Gwangju"
            };
            InitCity = new List<bool>
            {
                false, false, false,
                false, false, false,
            };
        }
    }
}
