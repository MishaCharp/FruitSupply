using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using FruitSupply.Domain.Models;
using FruitSupply.Persistance.Repositories;
using FruitSupply.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using TestApp.Persistence.Repositories.Base;

namespace FruitSupply.ViewModels
{
    public class SupplyPresenter : ReactiveObject
    {
        public readonly Guid guid;
        private readonly ICalculateService calculateService;
        public SupplyPresenter()
        {
            guid = Guid.NewGuid();
            calculateService = Locator.Current.GetService<ICalculateService>();
            calculateService.OnError += CalculateService_OnError;

            this.WhenAnyValue(x => x.SelectedProduct, x => x.SelectedSupplier, x => x.SelectedUnit, x => x.CountUnit)
                .Subscribe(x =>
                {
                    calculateService.Change(guid, x.Item2, x.Item1, x.Item3, x.Item4);
                });
        }

        private void CalculateService_OnError(Guid guid, string message)
        {
            if (this.guid != guid) return;

            if (message == "") IsError = false;
            else
            {
                IsError = true;
                ErrorText = message;
            }
        }

        public List<Supplier> AllSupplier { get; set; } = Locator.Current.GetService<IRepository<Supplier>>().GetList();
        [Reactive] public Supplier SelectedSupplier { get; set; }
        public List<Product> AllProduct { get; set; } = Locator.Current.GetService<IRepository<Product>>().GetList();
        [Reactive] public Product SelectedProduct { get; set; }
        public List<Unit> AllUnit { get; set; } = Locator.Current.GetService<IRepository<Unit>>().GetList();
        [Reactive] public Unit SelectedUnit { get; set; }
        [Reactive] public float CountUnit { get; set; } = 1;
        [Reactive] public string ErrorText { get; set; }
        [Reactive] public bool IsError { get; set; }
        public void DeleteSelf() => calculateService.DeletePosition(this);
    }
	public class AcceptanceGoodsViewModel : ViewModelBase
	{
        private readonly IRepository<Supply> supplyRepository;
        private readonly IRepository<SupplyDetail> supplyDetailRepository;

        private readonly ICalculateService calculateService;
        public AcceptanceGoodsViewModel()
        {
            calculateService = Locator.Current.GetService<ICalculateService>();
            supplyRepository = Locator.Current.GetService<IRepository<Supply>>();
            supplyDetailRepository = Locator.Current.GetService<IRepository<SupplyDetail>>();

            calculateService.OnCostChanges += CalculateService_OnCostChanges;
            calculateService.OnDeletePosition += (p) => Supplies.Remove(p);

            Supplies.Add(new());
        }

        private void CalculateService_OnCostChanges(decimal value)
        {
            TotalCost = value;
        }

        public string CurrentDate { get; set; } = DateTime.Now.Date.ToString("D");
        [Reactive] public decimal TotalCost { get; set; }
        [Reactive] public ObservableCollection<SupplyPresenter> Supplies { get; set; } = new();

        public void AddNewSupply()
        {
            Supplies.Add(new());
        }

        public async void AcceptSupply()
        {
            if (Supplies.Where(x => x.IsError).Count() > 0)
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка!", "Проверьте приёмку на наличие ошибок!").ShowAsync();
                return;
            }
            else if(Supplies
                .Count(x=>x.SelectedUnit !=null && x.SelectedSupplier!=null && x.SelectedUnit!=null) == 0)
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка!", "Добавьте хотя-бы одну позицию!").ShowAsync();
                return;
            }
            else
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Ошибка!", "Вы уверены, что хотите добавить приёмку?", MsBox.Avalonia.Enums.ButtonEnum.OkCancel);
                var result = await box.ShowAsync();
                if(result == ButtonResult.Ok)
                {
                    foreach (var supply in Supplies)
                    {
                        AddSupply(supply);
                    }
                    MessageBoxManager.GetMessageBoxStandard("Успешно!", $"Поставки были добавлены!\nКол-во поставок: [{Supplies.Count}]").ShowAsync();
                    Supplies.Clear();
                    calculateService.ClearAll();
                }
            }
        }

        private void AddSupply(SupplyPresenter supply)
        {
            var newSupply = new Supply()
            {
                DeliveryDate = DateTime.Now.Date,
                Supplier = supply.SelectedSupplier
            };

            supplyRepository.Create(newSupply);

            var supplyDetail = new SupplyDetail()
            {
                Supply = newSupply,
                Product = supply.SelectedProduct,
                Unit = supply.SelectedUnit,
                UnitCount = supply.CountUnit,
                TotalCost = calculateService.GetCost(supply.guid)
            };

            supplyDetailRepository.Create(supplyDetail);

        }

    }
}