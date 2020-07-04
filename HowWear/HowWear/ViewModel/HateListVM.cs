using HowWear.Model;
using HowWear.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowWear.ViewModel
{
    class HateListVM
    {
        public HateListModel HateModel { get; set; }
        public DeleteHateCombiCommand DeleteCommand { get; set; }
        public List<Pair<string, string>> Colors { get; set; }
        public List<Pair<string, string>> Type { get; set; }
        public HateListVM()
        {
            HateModel = new HateListModel();
            DeleteCommand = new DeleteHateCombiCommand(this);
            using (StreamReader reader = new StreamReader("Text/hateCombination.txt"))
            {
                while (reader.Peek() > 0)
                {
                    List<string> tmp = new List<string>();
                    ClothesViewInfo view = new ClothesViewInfo();
                    Colors = new List<Pair<string, string>>();
                    Type = new List<Pair<string, string>>();
                    SetClothesList();
                    string[] values = reader.ReadLine().Split(',');
                    foreach (var str in values)
                    {
                        if (str.Equals(" ")) tmp.Add(null);
                        else
                        {
                            string[] clothes = str.Remove(str.Length-1, 1).Split(' ');
                            string new_str = $"{getColor(clothes[0])} {getType(clothes[1])}";
                            tmp.Add(new_str);
                        }
                    }
                    view.Outer = tmp[0];
                    view.Top1 = tmp[1];
                    view.Top2 = tmp[2];
                    view.Bottom = tmp[3];
                    HateModel.HateList.Add(view);
                }
            }
        }

        public void deleteList(string outer, string top1, string top2, string bottom)
        {
            string key = "";
            StringBuilder returnWords = new StringBuilder();
            using (StreamReader reader = new StreamReader("Text/hateCombination.txt"))
            {
                while (reader.Peek() > 0)
                {
                    string words = reader.ReadLine();
                    int cnt = 0;
                    if (outer == null || words.Contains(getClothes(outer))) cnt++;
                    if (top1 == null || words.Contains(getClothes(top1))) cnt++;
                    if (top2 == null || words.Contains(getClothes(top2))) cnt++;
                    if (bottom == null || words.Contains(getClothes(bottom))) cnt++;
                    if (cnt == 4)
                    {
                        key = words;
                        break;
                    }
                }
            }
            using (StreamReader reader = new StreamReader("Text/hateCombination.txt"))
            {
                while(reader.Peek() > 0)
                {
                    string words = reader.ReadLine();
                    if (words.Equals(key)) continue;
                    returnWords.Append($"{words}\r\n");
                }
            }
            using (StreamWriter writer = new StreamWriter("Text/hateCombination.txt", false))
            {
                writer.Write(returnWords);
            }
        }

        private string getClothes(string str)
        {

            string[] values = str.Split(' ');
            string color = getColor(values[0], false);
            string type = getType(values[1], false);
            string clothes = $"{color} {type}";
            return clothes;
        }
        private string getColor(string str, bool flag = true)
        {
            if (flag)
            {
                foreach (var color in Colors)
                {
                    if (color.First.Equals(str)) return color.Second;
                }
            }
            else
            {
                foreach (var color in Colors)
                {
                    if (color.Second.Equals(str)) return color.First;
                }
            }
            return null;
        }
        private string getType(string str, bool flag = true)
        {
            if (flag)
            {
                foreach (var type in Type)
                {
                    if (type.First.Equals(str)) return type.Second;
                }
            }
            else
            {
                foreach (var type in Type)
                {
                    if (type.Second.Equals(str)) return type.First;
                }
            }
            return null;
        }

        private void SetClothesList()
        {
            Colors = new List<Pair<string, string>>
            {
                new Pair<string, string> ( "beige", "베이지" ),
                new Pair<string, string> ( "brown", "브라운" ),
                new Pair<string, string> ( "yellow", "노랑" ),
                new Pair<string, string> ( "khaki", "카키" ),
                new Pair<string, string> ( "navy", "네이비" ),
                new Pair<string, string> ( "white", "흰색" ),
                new Pair<string, string> ( "gray", "회색" ),
                new Pair<string, string> ( "black", "검정" ),
                new Pair<string, string> ( "red", "빨강" ),
                new Pair<string, string> ( "orange", "주황" ),
                new Pair<string, string> ( "green", "초록" ),
                new Pair<string, string> ( "aqua", "민트" ),
                new Pair<string, string> ( "skyblue", "하늘색" ),
                new Pair<string, string> ( "blue", "파랑" ),
                new Pair<string, string> ( "purple", "보라" ),
                new Pair<string, string> ( "pink", "핑크" ),
                new Pair<string, string> ( "light_blue", "연한" ),
                new Pair<string, string> ( "deep_blue", "진한" )
            };
            Type = new List<Pair<string, string>>
            {
                new Pair<string, string> ( "long-padding", "롱패딩" ),
                new Pair<string, string> ( "short-padding", "숏패딩" ),
                new Pair<string, string> ( "long-coat", "롱코트" ),
                new Pair<string, string> ( "short-coat", "숏코트" ),
                new Pair<string, string> ( "vest-padding", "조끼패딩" ),
                new Pair<string, string> ( "cardigan", "가디건" ),
                new Pair<string, string> ( "fleece", "후리스" ),
                new Pair<string, string> ( "jacket", "자켓" ),
                new Pair<string, string> ( "zip_up_hoodie", "후드집업" ),

                new Pair<string, string> ( "short-shirt", "반팔셔츠" ),
                new Pair<string, string> ( "turtleneck", "목티" ),
                new Pair<string, string> ( "knit", "니트" ),
                new Pair<string, string> ( "vest", "조끼" ),
                new Pair<string, string> ( "hood-T", "후드티" ),
                new Pair<string, string> ( "mantoman", "맨투맨" ),
                new Pair<string, string> ( "shirt", "셔츠" ),
                new Pair<string, string> ( "long-T", "긴팔" ),
                new Pair<string, string> ( "short-T", "반팔" ),

                new Pair<string, string> ( "jean", "청바지" ),
                new Pair<string, string> ( "slacks", "슬랙스" ),
                new Pair<string, string> ( "jogger", "조거팬츠" ),
                new Pair<string, string> ( "casual-pants", "면바지" ),
                new Pair<string, string> ( "short-pants", "반바지" ),

                new Pair<string, string>("long-flared_skirt", "롱 플레어 스커트"),
                new Pair<string, string>("long-H_skirt", "롱 H 스커트"),
                new Pair<string, string>("short-flared_skirt", "숏 플레어 스커트"),
                new Pair<string, string>("short-H_skirt", "숏 H 스커트"),
            };
        }
    }
}
