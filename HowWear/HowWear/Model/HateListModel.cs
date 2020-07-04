using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowWear.Model
{
    class HateListModel
    {
        public ObservableCollection<ClothesViewInfo> HateList { get; set; }
        public HateListModel()
        {
            HateList = new ObservableCollection<ClothesViewInfo>();
        }
    }
    public struct ClothesViewInfo
    {
        public string Outer { get; set; }
        public string Top1 { get; set; }
        public string Top2 { get; set; }
        public string Bottom { get; set; }
    }
}
