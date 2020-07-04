using HowWear.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    public class ImageVisibilityCommand : ICommand
    {
        public ClothesInfo M { get; set; }
        public event EventHandler CanExecuteChanged;
   
        public ImageVisibilityCommand(ClothesInfo m)
        {
            M = m;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (M.ImageVisible == Visibility.Visible)
            {
                M.ImageVisible = Visibility.Hidden;
                M.ColorVisible = Visibility.Visible;
            }
            else
            {
                M.ImageVisible = Visibility.Visible;
                M.ColorVisible = Visibility.Hidden;
            }
        }
    }
}
