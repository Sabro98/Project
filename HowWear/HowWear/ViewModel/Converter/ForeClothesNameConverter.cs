using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class ForeClothesNameConverter : IValueConverter
    {
        public List<Pair<string, string>> Type { get; set; }
        public ForeClothesNameConverter()
        {
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

                new Pair<string, string> ( "turtleneck", "목티" ),
                new Pair<string, string> ( "knit", "니트" ),
                new Pair<string, string> ( "vest", "조끼" ),
                new Pair<string, string> ( "hood-T", "후드티" ),
                new Pair<string, string> ( "mantoman", "맨투맨" ),
                new Pair<string, string> ( "shirt", "셔츠" ),
                new Pair<string, string> ( "long-T", "긴팔" ),
                new Pair<string, string> ( "short-T", "반팔" ),
                new Pair<string, string> ( "short-shirt", "반팔셔츠" ),

                new Pair<string, string> ( "jean", "청바지" ),
                new Pair<string, string> ( "slacks", "슬랙스" ),
                new Pair<string, string> ( "jogger", "조거팬츠" ),
                new Pair<string, string> ( "casual-pants", "면바지" ),
                new Pair<string, string> ( "short-pants", "반바지" )
            };
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if ((value as string).Contains("no")) return "옷 종류 부족 ㅠ_ㅠ";
                var str = (value as string).Split(' ')[1];
                foreach(var type in Type)
                {
                    if (type.First.Equals(str))
                    {
                        return type.Second;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
