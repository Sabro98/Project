using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HowWear.ViewModel.Converter
{
    class ForecastClothesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = "";
            string val = value as string;
            if (val == null) return null;
            if (val.Contains("cardigan")) path = "/TopImages/cardigan.png";
            else if (val.Contains("fleece")) path = "/TopImages/fleece.png";
            else if (val.Contains("short-shirt")) path = "/TopImages/short-shirt.png";
            else if (val.Contains("hood-T")) path = "/TopImages/hood-T.png";
            else if (val.Contains("jacket")) path = "/TopImages/jacket.png";
            else if (val.Contains("knit")) path = "/TopImages/knit.png";
            else if (val.Contains("long-T")) path = "/TopImages/long-T.png";
            else if (val.Contains("long-coat")) path = "/TopImages/long-coat.png";
            else if (val.Contains("long-padding")) path = "/TopImages/long-padding.png";
            else if (val.Contains("mantoman")) path = "/TopImages/mantoman.png";
            else if (val.Contains("shirt")) path = "/TopImages/shirt.png";
            else if (val.Contains("short-coat")) path = "/TopImages/short-coat.png";
            else if (val.Contains("short-padding")) path = "/TopImages/short-padding.png";
            else if (val.Contains("short-T")) path = "/TopImages/short-T.png";
            else if (val.Contains("turtleneck")) path = "/TopImages/turtleneck.png";
            else if (val.Contains("vest")) path = "/TopImages/vest.png";
            else if (val.Contains("zip_up_hoodie")) path = "/TopImages/zip_up_hoodie.png";
            else if (val.Contains("vest-padding")) path = "/TopImages/vest-padding.png";
            else path = "/Resources/noClothes.png";
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
