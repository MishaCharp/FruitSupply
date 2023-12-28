using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FruitSupply.Domain.Models;
using FruitSupply.Persistance.Repositories;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using TestApp.Persistence.Repositories.Base;

namespace FruitSupply.ViewModels
{
    public class UnitSumResult
    {
        public Unit Unit { get; set; }
        public float MaxUnit { get; set; }
        public decimal FullSumUnit { get; set; }
    }
    public class ReportPresenter
    {
        public Supplier Supplier { get; set; }
        public decimal TotalCost { get; set; }

        public List<UnitSumResult> UnitSumResults { get; set; } = new();
        public List<SupplyDetail> SupplyDetails { get; set; } = new();
    }

	public class ReportViewModel : ViewModelBase
	{
        private readonly IRepository<SupplyDetail> supplyDetailRepository;
        private readonly IRepository<Supply> supplyRepository;

		[Reactive] public DateTimeOffset DateStart { get; set; } = new DateTimeOffset(DateTime.Today);
        [Reactive] public DateTimeOffset DateEnd { get; set; } = new DateTimeOffset(DateTime.Today);

        [Reactive] public ObservableCollection<ReportPresenter> ReportPresenters { get; set; } = new();
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> GetReport { get; set; }

        public ReportViewModel()
        {
            supplyDetailRepository = Locator.Current.GetService<IRepository<SupplyDetail>>();
            supplyRepository = Locator.Current.GetService<IRepository<Supply>>();

            GetReport = ReactiveCommand.Create(getReport,
                this.WhenAnyValue(x => x.DateStart, x => x.DateEnd, 
                (start, end) => start <= end ));
        }

        public void getReport()
        {
            ReportPresenters.Clear();

            var filteredSupplies = supplyRepository
                .GetList()
                .Where(x=>x.DeliveryDate >= DateStart && x.DeliveryDate <= DateEnd);

            var groupSupplies = filteredSupplies.GroupBy(x => x.Supplier);

            foreach (var group in groupSupplies)
            {
                var idsSuply = group.Select(x => x.Id).ToList();
                var allDetails = supplyDetailRepository.GetList().Where(x => idsSuply.Contains(x.Supply.Id)).ToList();

                var report = new ReportPresenter();
                report.Supplier = group.Key;
                report.TotalCost = allDetails.Sum(x => x.TotalCost);
                report.SupplyDetails = allDetails;

                var groupsUnit = allDetails.GroupBy(x => x.Unit);
                var unitsResult = new List<UnitSumResult>();
                foreach(var unitGroup in groupsUnit)
                {
                    var unitRes = new UnitSumResult()
                    {
                        Unit = unitGroup.Key,
                        FullSumUnit = unitGroup.Sum(x => x.TotalCost),
                        MaxUnit = unitGroup.Sum(x => x.UnitCount)
                    };
                    unitsResult.Add(unitRes);
                }

                report.UnitSumResults = unitsResult;

                ReportPresenters.Add(report);
            };

        }

    }
}