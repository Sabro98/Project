using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HowWear.ViewModel.Converter
{
    class ClothesImageConverter : IValueConverter
    {

        private string[] outers = {"long-padding", "short-padding", "long-coat", "short-coat", "vest-padding", "cardigan",
                                    "fleece", "jacket", "zip_up_hoodie" };
        private string[] tops = {"short-shirt", "turtleneck", "knit", "vest", "hood-T", "mantoman", "shirt", "long-T",
                                    "short-T" };
        private string[] bottoms = { "jean", "slacks", "jogger", "casual-pants", "short-pants" };
        private string[] skirts = { "long-flared_skirt", "long-H_skirt", "short-flared_skirt", "short-H_skirt" };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if(str != null)
            {
                //아우터
                for (int i = 0; i < outers.Length; i++)
                    if (str.Contains(outers[i]))
                        return $"/TopImages/{outers[i]}.png";

                for (int i = 0; i < tops.Length; i++)
                    if (str.Contains(tops[i]))
                        return $"/TopImages/{tops[i]}.png";

                for (int i = 0; i < bottoms.Length; i++)
                    if (str.Contains(bottoms[i]))
                        return $"/TopImages/{bottoms[i]}.png";

                for (int i = 0; i < skirts.Length; i++)
                    if (str.Contains(skirts[i]))
                        return $"/TopImages/{skirts[i]}.png";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
