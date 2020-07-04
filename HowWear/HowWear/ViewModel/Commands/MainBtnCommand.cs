using HowWear.View;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    class MainBtnCommand : ICommand
    {
        public MainVM VM { get; set; }
        public MainBtnCommand(MainVM vm)
        {
            VM = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string content = parameter as string;

            if (content.Equals("clothesPrevBtn"))
            {
                if (VM.ClothesCnt != 0) VM.MakeLook(--VM.ClothesCnt);
                
            }else if (content.Equals("clothesNextBtn"))
            {
                if (VM.ClothesCnt != VM.ClothesSet.Count - 1) VM.MakeLook(++VM.ClothesCnt);
            }
            else if (content.Equals("prevBtn"))
            {
                if (VM.Cnt != 0)
                {
                    VM.SetInfo(--VM.Cnt);
                }
            }
            else if(content.Equals("nextBtn"))
            {
                if (VM.Cnt != 3)
                {
                    VM.SetInfo(++VM.Cnt);
                }
            }
            else if (content.Equals("SettingBtn"))
            {
                Option option = new Option();
                option.ShowDialog();
                VM.RenewFiles();
                VM.GetWeather();
                VM.GetFWeather();
                VM.SetClothes();
            }
            else if (content.Equals("RandomBtn"))
            {
                var rand = new Random();
                VM.ClothesCnt = rand.Next(VM.ClothesSet.Count);
                VM.MakeLook(VM.ClothesCnt);
            }else if (content.Equals("listBtn"))
            {
                HateListView view = new HateListView();
                view.ShowDialog();
                VM.RenewFiles();
                VM.SetClothes();
            }
        }
    }
}
