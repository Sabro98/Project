using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HowWear.ViewModel.Commands
{
    class CityCheckCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public LocationVM VM { get; set; }
  
        public CityCheckCommand(LocationVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                string location = (parameter as TextBox).Text;
                if (MainVM.TestAPI(location))
                {
                    VM.PossibleLocation = true;
                    VM.CheckEnable = false;
                    (parameter as TextBox).BorderBrush = System.Windows.Media.Brushes.Green;
                }
                else VM.PossibleLocation = false;
            }
        }
    }
}
