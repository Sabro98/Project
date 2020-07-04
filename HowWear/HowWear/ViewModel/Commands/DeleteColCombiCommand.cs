using HowWear.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    class DeleteColCombiCommand : ICommand
    {
        public MainVM VM { get; set; }
        public DeleteColCombiCommand(MainVM vm)
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
            if (!(VM.ClothesInfo.Outer == null && VM.ClothesInfo.Top1 == null &&
                VM.ClothesInfo.Top2 == null && VM.ClothesInfo.Bottom == null))
            {
                using (StreamWriter writer = new StreamWriter("Text/hateCombination.txt", true))
                {
                    StringBuilder ret = new StringBuilder();
                    List<string> combi = new List<string>
                    {
                        VM.ClothesInfo.Outer,
                        VM.ClothesInfo.Top1,
                        VM.ClothesInfo.Top2,
                        VM.ClothesInfo.Bottom
                    };
                    foreach (var clothes in combi)
                    {
                        if (clothes == null)
                        {
                            ret.Append(" ,");
                        }
                        else
                        {
                            ret.Append($"{clothes} ,");
                        }
                    }
                    ret.Remove(ret.Length - 1, 1);
                    writer.WriteLine(ret);
                }
                VM.RenewFiles();
                VM.SetClothes();
            }
            
        }
    }
}
