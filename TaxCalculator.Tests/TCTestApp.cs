using System;
using DryIoc;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using TaxCalculator.Services;
using TaxCalculator.Tests;
using TaxCalculator.ViewModels;
using TaxCalculator.Views;
using Xamarin.Forms;

public partial class TCTestApp : PrismApplication
{
    public TCTestApp() : this(null) { }

    public TCTestApp(IPlatformInitializer initializer) : base(initializer) { }

    protected override void OnInitialized()
    {
        //Could navigate to our page but we are testing the Viewmodel and not the view itselfe.
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<NavigationPage>();
        containerRegistry.RegisterForNavigation<TaxCalculationPage, TaxCalculationPageViewModel>();

        containerRegistry.RegisterSingleton<ITaxJarClient, MockTaxJarService>();
        containerRegistry.Register<ITaxService, TaxService>();
    }
}

