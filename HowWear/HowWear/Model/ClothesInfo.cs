using HowWear.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HowWear.Model
{
    public class ClothesInfo : INotifyPropertyChanged
    {
        public ClothesInfo(string name)
        {
            VCommand = new ImageVisibilityCommand(this);
            Name = name;
            ImageVisible = Visibility.Visible;
            ColorVisible = Visibility.Hidden;
            ImagePath = $"/TopImages/{name}.png";
            Check = new ObservableCollection<bool>();
            for (int i = 0; i < 15; i++) Check.Add(false);
        }

        public ImageVisibilityCommand VCommand { get; set; }
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
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public ObservableCollection<bool> Check { get; set; }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }


        private Visibility imageVisible;

        public Visibility ImageVisible
        {
            get { return imageVisible; }
            set { imageVisible = value; OnPropertyChanged(nameof(ImageVisible)); }
        }

        private Visibility colorVisible;

        public Visibility ColorVisible
        {
            get { return colorVisible; }
            set { colorVisible = value; OnPropertyChanged(nameof(ColorVisible)); }
        }
    }
}
