using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace HowWear.Model
{
    public class PersonalInfo : INotifyPropertyChanged
    {
        public PersonalInfo()
        {
            Gender = new ObservableCollection<bool>
            {
                false, false
            };
            Characteristic = new ObservableCollection<bool>()
            {
                false, false, false
            };
            FavCol = new ObservableCollection<bool>()
            {
                false, false, false, false, false,
                false, false, false, false, false,
                false, false, false, false, false
            };
            FavClothes = new ObservableCollection<bool>()
            {
                false, false, false, false, false, false,
                false, false, false
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<bool> Gender { get; set; }

        public ObservableCollection<bool> Characteristic { get; set; }

        public ObservableCollection<bool> FavCol { get; set; }

        public ObservableCollection<bool> FavClothes { get; set; }

    }
}
