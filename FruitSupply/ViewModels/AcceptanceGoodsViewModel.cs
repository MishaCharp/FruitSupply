using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FruitSupply.ViewModels
{
    public class SupplyPresenter
    {
        
    }
	public class AcceptanceGoodsViewModel : ViewModelBase
	{
        public string CurrentDate { get; set; } = DateTime.Now.Date.ToString("D");
        [Reactive] public decimal TotalCost { get; set; }
        [Reactive] public ObservableCollection<SupplyPresenter> Supplies { get; set; } = new();

        public AcceptanceGoodsViewModel()
        {
            Supplies.Add(new());
        }

        public void AddNewSupply()
        {
            Supplies.Add(new());
        }

    }
}