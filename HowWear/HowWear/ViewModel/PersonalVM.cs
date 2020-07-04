using HowWear.Model;
using HowWear.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HowWear.ViewModel
{
    public class PersonalVM
    {
        private string[] colors = {"red", "orange", "yellow", "beige", "brown", "green", "aqua", "skyblue", "blue", "navy"
                                    , "purple", "pink", "white", "gray", "black"};
        private string[] clothes = { "turtleneck", "knit", "vest", "hood-T", "mantoman", "shirt", "long-T", "short-T", "short-shirt" };
    
        public PersonalInfo personal { get; set; }
        public PersonalSaveCommand SaveCommand { get; set; }

        public PersonalVM()
        {
            personal = new PersonalInfo();
            SaveCommand = new PersonalSaveCommand(this);
            Check();
        }

        private void Check()
        {

            using (StreamReader reader = new StreamReader("Text/gender.txt"))
            {
                string tmp = reader.ReadLine();
                if (tmp.Equals("male")) personal.Gender[0] = true;
                else if (tmp.Equals("female")) personal.Gender[1] = true;
            }
            using (StreamReader reader = new StreamReader("Text/characteristic.txt"))
            {
                string tmp = reader.ReadLine();
                if (tmp.Equals("cold")) personal.Characteristic[0] = true;
                else if (tmp.Equals("general")) personal.Characteristic[1] = true;
                else if (tmp.Equals("hot")) personal.Characteristic[2] = true;
            }
            using (StreamReader reader = new StreamReader("Text/favoriteColor.txt"))
            {
                while (reader.Peek() > 0)
                {
                    string tmp = reader.ReadLine();
                    for(int i=0; i<colors.Length; i++)
                    {
                        if (tmp.Equals(colors[i])) personal.FavCol[i] = true;
                    }
                }
            }
                
            using (StreamReader reader = new StreamReader("Text/favoriteClothes.txt"))
            {

                while(reader.Peek() > 0)
                {
                    string tmp = reader.ReadLine();
                    for (int i = 0; i < personal.FavClothes.Count; i++)
                        if (tmp.Equals(clothes[i])) personal.FavClothes[i] = true;
                }
            }
            
        }
    }
}
