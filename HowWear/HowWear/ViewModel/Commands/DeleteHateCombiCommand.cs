using HowWear.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HowWear.ViewModel.Commands
{
    class DeleteHateCombiCommand : ICommand
    {
        public HateListVM VM { get; set; }
        public DeleteHateCombiCommand(HateListVM vm)
        {
            VM = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
            {
                var value = (ClothesViewInfo)parameter;
                
                for (int i = 0; i < VM.HateModel.HateList.Count; i++)
                {
                    var tmp = VM.HateModel.HateList[i];
                    int cnt = 0;
                    if (tmp.Outer != null && value.Outer != null)
                    {
                        if (tmp.Outer.Equals(value.Outer)) cnt++;
                    }
                    else if (tmp.Outer == null && value.Outer == null) cnt++;

                    if (tmp.Top1 != null && value.Top1 != null)
                    {
                        if (tmp.Top1.Equals(value.Top1)) cnt++;
                    }
                    else if (tmp.Top1 == null && value.Top1 == null) cnt++;

                    if (tmp.Top2 != null && value.Top2 != null)
                    {
                        if (tmp.Top2.Equals(value.Top2)) cnt++;
                    }
                    else if (tmp.Top2 == null && value.Top2 == null) cnt++;

                    if (tmp.Bottom != null && value.Bottom != null)
                    {
                        if (tmp.Bottom.Equals(value.Bottom)) cnt++;
                    }
                    else if (tmp.Bottom == null && value.Bottom == null) cnt++;

                    if (cnt == 4)
                    {
                        VM.deleteList(value.Outer, value.Top1, value.Top2, value.Bottom);
                        VM.HateModel.HateList.RemoveAt(i);
                    }
                }
                
            }
        }
    }
}
