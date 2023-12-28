using Avalonia.Controls;
using FruitSupply.Domain.Models;
using FruitSupply.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Persistence.Repositories.Base;

namespace FruitSupply.Services
{
    public class SupplyDTO
    {
        public Guid Guid { get; set; }
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
        public Unit Unit { get; set; }
        public float CountUnit { get; set; }
        public decimal TotalCost { get; set; } = 0;

        public SupplyDTO(Guid guid, Supplier supplier, Product product, Unit unit, float countUnit)
        {
            Guid = guid;
            Supplier = supplier;
            Product = product;
            Unit = unit;
            CountUnit = countUnit;
        }
    }

    public class CalculateService : ICalculateService
    {
        private readonly IRepository<SupplierPrice> SupplierPriceRepository;
        public CalculateService()
        {
            SupplierPriceRepository = Locator.Current.GetService<IRepository<SupplierPrice>>();
        }
        public event Action<decimal> OnCostChanges;
        public event Action<SupplyPresenter> OnDeletePosition;
        public event Action<Guid, string> OnError;

        private decimal cost;
        private decimal Cost
        {
            get => cost;
            set
            {
                cost = value;
                OnCostChanges?.Invoke(cost);
            }
        }
        private DateTime CurrentDate = DateTime.Now.Date;

        private List<SupplyDTO> SupplyList { get; set; } = new();
        public void Change(Guid guid, Supplier supplier, Product product, Unit unit, float countUnit)
        {
            var supply = SupplyList.FirstOrDefault(x => x.Guid == guid);
            if (supply == null) SupplyList.Add(new SupplyDTO(guid, supplier, product, unit, countUnit));
            else
            {
                supply.Supplier = supplier;
                supply.Product = product;
                supply.Unit = unit;
                supply.CountUnit = countUnit;

                ReCalc(supply);
            }

        }

        bool IsSomeNull(SupplyDTO supply)
        {
            if (supply.Supplier == null) return true;
            if (supply.Product == null) return true;
            if (supply.Unit == null) return true;

            return false;
        }

        private void ReCalc(SupplyDTO supply)
        {
            if (IsSomeNull(supply)) return;

            var allPrices = SupplierPriceRepository.GetList();
            var needSupplierPrice = allPrices.FirstOrDefault(x =>
            x.Supplier.Id == supply.Supplier.Id &&
            x.Product.Id == supply.Product.Id &&
            x.Unit.Id == supply.Unit.Id);

            if (needSupplierPrice != null)
            {
                if(CurrentDate >= needSupplierPrice.StartPeriod && CurrentDate <= needSupplierPrice.EndPeriod)
                {
                    Error(supply.Guid, "");//Сбрасываем ошибки

                    Cost -= supply.TotalCost;

                    var costPerUnit = needSupplierPrice.UnitPrice;
                    var newCost = costPerUnit * (decimal)supply.CountUnit;
                    supply.TotalCost = newCost;

                    Cost += newCost;
                }
                else
                {
                    Error(supply.Guid, "На текущую дату поставки цены на данный продукт не существует!");
                }

            }
            else
            {
                Error(supply.Guid, "Не найдена цена для данных параметров");
                if (supply.TotalCost > 0)
                {
                    Cost -= supply.TotalCost;
                    supply.TotalCost = 0;
                }
            }


        }

        public void Error(Guid guid, string message) => OnError?.Invoke(guid, message);
        public void DeletePosition(SupplyPresenter supply)
        {
            var Supply = SupplyList.FirstOrDefault(x => x.Guid == supply.guid);
            if (Supply != null)
            {
                Cost -= Supply.TotalCost;
                SupplyList.Remove(Supply);
            }
            OnDeletePosition?.Invoke(supply);
        }

        public decimal GetCost(Guid guid) => SupplyList.FirstOrDefault(x=>x.Guid == guid).TotalCost;

        public void ClearAll()
        {
            SupplyList.Clear();
            Cost = 0;
        }
    }
}
