using HowWear.Model;
using HowWear.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    class SettingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var values = (object[])parameter;
            var content = values[0] as string;
            var window = values[1] as Window;

            if (content.Equals("특성 고르기!"))
            {
                PersonalSetting personal = new PersonalSetting();
                window.Close();
                personal.ShowDialog();
            }
            else if(content.Equals("옷 고르기!"))
            {
                ClothesSetting clothes = new ClothesSetting();
                window.Close();
                clothes.ShowDialog();
            }
            else if(content.Equals("위치 선택!"))
            {
                LocationSetting location = new LocationSetting();
                window.Close();
                location.ShowDialog();
            }
        }
    }
}
