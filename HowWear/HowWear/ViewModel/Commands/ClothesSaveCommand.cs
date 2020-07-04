using HowWear.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    public class ClothesSaveCommand : ICommand
    {
        public ClothesVM VM { get; set; }
        public event EventHandler CanExecuteChanged;
        private readonly string[] OuterClolors = { "beige", "brown", "yellow", "khaki", "navy", "white", "gray", "black" };
        private readonly string[] Topcolors = {"red", "orange", "yellow", "beige", "brown", "green", "aqua", "skyblue", "blue", "navy"
                                    , "purple", "pink", "white", "gray", "black"};
        private readonly string[] BottomClolors = {"beige", "brown", "light_blue", "deep_blue", "khaki", "white", "gray", "black" };
        private readonly string[] SkirtColors = { "beige", "brown", "navy", "blue", "skyblue", "white", "gray", "black"};
        public ClothesSaveCommand(ClothesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (StreamWriter writer = new StreamWriter("Text/outer.txt", false))
            {
                foreach (var info in VM.OuterInfo)
                {
                    for (int i = 0; i < info.Check.Count; i++)
                    {
                        if (info.Check[i]) writer.WriteLine($"{OuterClolors[i]} {info.Name}");
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter("Text/top.txt", false))
            {

                foreach (ClothesInfo info in VM.TopInfo)
                {
                    for (int i = 0; i < info.Check.Count; i++)
                    {
                        if (info.Check[i]) writer.WriteLine($"{Topcolors[i]} {info.Name}");
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter("Text/bottom.txt", false))
            {
                foreach (var info in VM.BottomInfo)
                {
                    for (int i = 0; i < info.Check.Count; i++)
                    {
                        if (info.Check[i]) writer.WriteLine($"{BottomClolors[i]} {info.Name}");
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter("Text/skirt.txt", false))
            {
                foreach(var info in VM.SkirtInfo)
                {
                    for(int i=0; i<info.Check.Count; i++)
                    {
                        if (info.Check[i]) writer.WriteLine($"{SkirtColors[i]} {info.Name}");
                    }
                }
            }

            (parameter as Window).Close();
        }
    }
}
