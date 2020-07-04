using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HowWear.ViewModel
{
    public class ClothesTextBlock : TextBlock
    {


        public string ClothesName
        {
            get { return (string)GetValue(ClothesNameProperty); }
            set { SetValue(ClothesNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClothesName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClothesNameProperty =
            DependencyProperty.Register("ClothesName", typeof(string), typeof(ClothesTextBlock), new PropertyMetadata(OnNameCol));

        private static void OnNameCol(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ClothesTextBlock block = source as ClothesTextBlock;
            SolidColorBrush col = null;
            if (block.ClothesName != null)
            {
                string color = (block.ClothesName as string).Split(' ')[0];

                if (color.Equals("beige")) col = Brushes.Beige;
                else if (color.Equals("brown")) col = Brushes.Brown;
                else if (color.Equals("yellow")) col = Brushes.Yellow;
                else if (color.Equals("khaki")) col = Brushes.Khaki;
                else if (color.Equals("navy")) col = Brushes.Navy;
                else if (color.Equals("white")) col = Brushes.White;
                else if (color.Equals("gray")) col = Brushes.DarkGray;
                else if (color.Equals("black")) col = Brushes.Black;
                else if (color.Equals("red")) col = Brushes.Red;
                else if (color.Equals("orange")) col = Brushes.Orange;
                else if (color.Equals("green")) col = Brushes.Green;
                else if (color.Equals("aqua")) col = Brushes.Aquamarine;
                else if (color.Equals("skyblue")) col = Brushes.SkyBlue;
                else if (color.Equals("blue")) col = Brushes.Blue;
                else if (color.Equals("purple")) col = Brushes.Purple;
                else if (color.Equals("pink")) col = Brushes.LightPink;
                else if (color.Equals("light_blue")) col = Brushes.LightBlue;
                else if (color.Equals("deep_blue")) col = Brushes.DarkBlue;
            }
            if(col != null) block.Foreground = col;
        }
    }
}
