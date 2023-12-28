using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSupply.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public AcceptanceGoodsViewModel AcceptanceGoodsControl { get; set; }
        public ReportViewModel ReportControl { get; set; }

        public MainWindowViewModel()
        {
            AcceptanceGoodsControl = new();
            ReportControl = new();
        }
    }
}
