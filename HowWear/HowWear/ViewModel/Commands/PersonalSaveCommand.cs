using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    public class PersonalSaveCommand : ICommand
    {        
        public PersonalVM VM { get; set; }
        public event EventHandler CanExecuteChanged;
        public string[][] PersonalString;
        public PersonalSaveCommand(PersonalVM vm)
        {
            VM = vm;
            PersonalString = new string[4][]
            {
                new string[] {"male", "female" },
                new string[] {"cold", "general", "hot" },
                new string[] {"red", "orange", "yellow", "beige", "brown", "green", "aqua", "skyblue", "blue", "navy"
                                    , "purple", "pink", "white", "gray", "black"},
                new string[] { "turtleneck", "knit", "vest", "hood-T", "mantoman", "shirt", "long-T", "short-T", "short-shirt" }
            };
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
            {
                using (StreamWriter writer = new StreamWriter("Text/gender.txt", false))
                {
                    for (int i = 0; i < VM.personal.Gender.Count; i++)
                        if (VM.personal.Gender[i]) writer.WriteLine(PersonalString[0][i]);
                }
                using (StreamWriter writer = new StreamWriter("Text/characteristic.txt", false))
                {
                    for (int i = 0; i < VM.personal.Characteristic.Count; i++)
                        if (VM.personal.Characteristic[i]) writer.WriteLine(PersonalString[1][i]);
                }
                using (StreamWriter writer = new StreamWriter("Text/favoriteColor.txt", false))
                {
                    for (int i = 0; i < VM.personal.FavCol.Count; i++)
                        if (VM.personal.FavCol[i]) writer.WriteLine(PersonalString[2][i]);
                }
                using (StreamWriter writer = new StreamWriter("Text/favoriteClothes.txt", false))
                {
                    for (int i = 0; i < VM.personal.FavClothes.Count; i++)
                        if (VM.personal.FavClothes[i]) writer.WriteLine(PersonalString[3][i]);
                }
                (parameter as Window).Close();
                }
        }
       
    }
}
