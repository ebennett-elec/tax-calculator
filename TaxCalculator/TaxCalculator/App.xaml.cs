using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using TaxCalculator.ViewModels;
using TaxCalculator.Views;
using Prism.Ioc;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public partial class App : PrismApplication, IObserver<Exception>
    {
        public App() 
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var _ = await NavigationService.NavigateAsync("NavigationPage/TaxCalculationPage");
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterSingleton<ITaxJarClient, TaxJarClient>();
            containerRegistry.Register<ITaxService, TaxService>();
            containerRegistry.RegisterForNavigation<TaxCalculationPage, TaxCalculationPageViewModel>();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Exception value)
        {
            throw new NotImplementedException();
        }
    }
}

