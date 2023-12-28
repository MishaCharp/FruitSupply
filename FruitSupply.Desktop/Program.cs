using System;

using Avalonia;
using Avalonia.ReactiveUI;
using FruitSupply.Domain.Models;
using FruitSupply.Persistance.Repositories;
using FruitSupply.Services;
using FruitSupply.ViewModels;
using FruitSupply.Views;
using ReactiveUI;
using Splat;
using TestApp.Persistence.Repositories.Base;

namespace FruitSupply.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {

        Locator.CurrentMutable.RegisterLazySingleton(() => new ProductRepository(), typeof(IRepository<Product>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new ProductGradeRepository(), typeof(IRepository<ProductGrade>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new ProductTypeRepository(), typeof(IRepository<ProductType>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new SupplierPriceRepository(), typeof(IRepository<SupplierPrice>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new SupplierRepository(), typeof(IRepository<Supplier>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new SupplyDetailRepository(), typeof(IRepository<SupplyDetail>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new SupplyRepository(), typeof(IRepository<Supply>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new UnitRepository(), typeof(IRepository<Unit>));

        Locator.CurrentMutable.RegisterLazySingleton(() => new CalculateService(), typeof(ICalculateService));

        //Locator.CurrentMutable.Register(() => new AcceptanceGoodsView(), typeof(IViewFor<AcceptanceGoodsViewModel>));

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }
}
