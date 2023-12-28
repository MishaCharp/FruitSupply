using Avalonia.Controls;
using FruitSupply.Domain.Models;
using FruitSupply.ViewModels;
using System;

namespace FruitSupply.Services
{
    public interface ICalculateService
    {
        public event Action<SupplyPresenter> OnDeletePosition;
        public void DeletePosition(SupplyPresenter supply);
        public void Error(Guid guid,string message);

        public event Action<decimal> OnCostChanges;
        public event Action<Guid,string> OnError;
        public void Change(Guid guid, Supplier supplier, Product product, Unit unit, float countUnit);
        public decimal GetCost(Guid guid);
        public void ClearAll();
    }
}