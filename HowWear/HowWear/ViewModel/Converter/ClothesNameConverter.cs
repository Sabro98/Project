using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    public class ClothesNameConverter : IValueConverter
    {
        public List<Pair<string, string>> Colors { get; set; }
        public List<Pair<string, string>> Type { get; set; }
        public ClothesNameConverter()
        {
            SetClothesList();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if ((value as string).Equals("종류 부족")) return "옷의 종류 부족ㅠ_ㅠ";
                string[] str = (value as string).Split(' ');
                string color = str[0];
                string type = str[1];
                string returnCol = "";
                string returnType = "";

                //색 판별
                foreach (var tmp in Colors)
                {
                    if (tmp.First.Equals(color))
                    {
                        returnCol = tmp.Second;
                        break;
                    }
                }

                //아우터, 상의 하의 판별
                foreach (var tmp in Type)
                {
                    if (tmp.First.Equals(type))
                    {
                        returnType = tmp.Second;
                        break;
                    }
                }

                var returnStr = $"{returnCol} {returnType}";

                return returnStr;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
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

