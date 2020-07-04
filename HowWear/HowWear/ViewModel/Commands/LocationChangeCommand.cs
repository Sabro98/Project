using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    class LocationChangeCommand : ICommand
    {
        public LocationVM VM { get; set; }
        public event EventHandler CanExecuteChanged;
        public LocationChangeCommand(LocationVM vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var values = (object[])parameter;
            var location = values[0] as string;
            var window = values[1] as Window;

            using (StreamWriter writer = new StreamWriter("Text/location.txt", false))
            {

                writer.WriteLine(location);
            }
            window.Close();
        }
    }
}
