using HowWear.Model;
using HowWear.View.Controls;
using HowWear.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HowWear.ViewModel
{
    public class ClothesVM
    {
        public ClothesSaveCommand SaveCommand { get; set; }
        public ObservableCollection<ClothesInfo> OuterInfo { get; set; }
        public ObservableCollection<ClothesInfo> TopInfo { get; set; }
        public ObservableCollection<ClothesInfo> BottomInfo { get; set; }
        public ObservableCollection<ClothesInfo> SkirtInfo { get; set; }
        public List<string> Outers { get; set; }
        public List<string> OuterColors { get; set; }
        public List<string> Tops { get; set; }
        public List<string> TopColors { get; set; }
        public List<string> Bottoms { get; set; }
        public List<string> BottomColors { get; set; }
        public List<string> Skirts { get; set; }
        public List<string> SkirtColors { get; set; }
        public ClothesVM()
        {
            SetClothesList();
            OuterInfo = new ObservableCollection<ClothesInfo>();
            TopInfo = new ObservableCollection<ClothesInfo>();
            BottomInfo = new ObservableCollection<ClothesInfo>();
            SkirtInfo = new ObservableCollection<ClothesInfo>();
            SaveCommand = new ClothesSaveCommand(this);

            foreach(var outer in Outers) OuterInfo.Add(new ClothesInfo(outer));
            for (int i = 0; i < OuterInfo.Count; i++) CheckOuterInfo(i);

            foreach (var top in Tops) TopInfo.Add(new ClothesInfo(top));
            for (int i = 0; i < TopInfo.Count; i++) CheckTopItem(i);

            foreach (var bottom in Bottoms) BottomInfo.Add(new ClothesInfo(bottom));
            for (int i = 0; i < BottomInfo.Count; i++) CheckBottomInfo(i);

            foreach (var skirt in Skirts) SkirtInfo.Add(new ClothesInfo(skirt));
            for (int i = 0; i < SkirtInfo.Count; i++) CheckSkirtInfo(i);
        }

        private void CheckOuterInfo(int cnt)
        {
            //베이지 브라운 노랑 카키 네이비 하얀 회색 검정
            using (StreamReader reader = new StreamReader("Text/outer.txt"))
            {
                while (reader.Peek() > 0)
                {
                    string tmp = reader.ReadLine();
                    if (tmp.Contains(OuterInfo[cnt].Name))
                    {
                        for(int i=0; i<OuterColors.Count; i++)
                        {
                            if (tmp.Contains(OuterColors[i]))
                            {
                                OuterInfo[cnt].Check[i] = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void CheckTopItem(int cnt)
        {
            using (StreamReader reader = new StreamReader("Text/top.txt"))
            {
                while (reader.Peek() > 0)
                {
                    string tmp = reader.ReadLine();
                    if (tmp.Contains(TopInfo[cnt].Name))
                    {
                        for (int i = 0; i < TopColors.Count; i++)
                        {
                            if (tmp.Contains(TopColors[i]))
                            {
                                TopInfo[cnt].Check[i] = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void CheckBottomInfo(int cnt)
        { 
            using (StreamReader reader = new StreamReader("Text/bottom.txt"))
            {
                while (reader.Peek() > 0)
                {
                    string tmp = reader.ReadLine();
                    if (tmp.Contains(BottomInfo[cnt].Name))
                    {
                        for (int i = 0; i < BottomColors.Count; i++)
                        {
                            if (tmp.Contains(BottomColors[i]))
                            {
                                BottomInfo[cnt].Check[i] = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void CheckSkirtInfo(int cnt)
        {
            using (StreamReader reader = new StreamReader("Text/skirt.txt"))
            {
                while (reader.Peek() > 0)
                {
                    string tmp = reader.ReadLine();
                    if (tmp.Contains(SkirtInfo[cnt].Name))
                    {
                        for (int i = 0; i < SkirtColors.Count; i++)
                        {
                            if (tmp.Contains(SkirtColors[i]))
                            {
                                SkirtInfo[cnt].Check[i] = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void SetClothesList()
        {
            Outers = new List<string>
            {
                "long-padding", "short-padding", "long-coat", "short-coat", "vest-padding", "cardigan",
                "fleece", "jacket", "zip_up_hoodie"
            };

            OuterColors = new List<string>
            {
                "beige", "brown", "yellow", "khaki", "navy", "white", "gray", "black"
            };

            Tops = new List<string>
            {
                "turtleneck", "knit", "vest", "hood-T", "mantoman", "shirt", "long-T",
                "short-T", "short-shirt"
            };

            TopColors = new List<string>
            {
                "red", "orange", "yellow", "beige", "brown", "green", "aqua", "skyblue", "blue", "navy",
                "purple", "pink", "white", "gray", "black"
            };

            Bottoms = new List<string>
            {
                "jean", "slacks", "jogger", "casual-pants", "short-pants"
            };

            BottomColors = new List<string>
            {
                 "beige", "brown", "light_blue", "deep_blue", "khaki", "white", "gray", "black"
            };

            Skirts = new List<string>
            {
                "long-flared_skirt", "long-H_skirt", "short-flared_skirt", "short-H_skirt"
            };

            SkirtColors = new List<string>
            {
                "beige", "brown", "navy", "blue", "skyblue", "white", "gray", "black"
            };
        }
    }
}
