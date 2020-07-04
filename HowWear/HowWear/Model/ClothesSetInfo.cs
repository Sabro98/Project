using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowWear.Model
{
    public class ClothesSetInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<string, int> ValueDic { get; set; }
        public Dictionary<string, List<string>> OuterBytop { get; set; }
        public Dictionary<string, List<string>> TopBytop { get; set; }
        public Dictionary<string, List<string>> TopBybottom { get; set; }
        public List<string> TopSolo { get; set; }
        public Dictionary<string, List<string>> TopBybottomCol { get; set; }
        public Dictionary<string, List<string>> OuterByTopCol { get; set; }
        public Dictionary<string, List<string>> OuterByBottom { get; set; }
        public Dictionary<string, List<string>> TopByskirt { get; set; }
        public Dictionary<string, List<string>> OuterBySkirt { get; set; }
        public Dictionary<string, List<string>> TopBySkirtCol { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string outer;

        public string Outer
        {
            get { return outer; }
            set { outer = value; OnPropertyChanged(nameof(Outer)); }
        }

        private string top1;

        public string Top1
        {
            get { return top1; }
            set { top1 = value; OnPropertyChanged(nameof(Top1)); }
        }

        private string top2;

        public string Top2
        {
            get { return top2; }
            set { top2 = value; OnPropertyChanged(nameof(Top2)); }
        }

        private string bottom;

        public string Bottom
        {
            get { return bottom; }
            set { bottom = value; OnPropertyChanged(nameof(Bottom)); }
        }
    }
}
