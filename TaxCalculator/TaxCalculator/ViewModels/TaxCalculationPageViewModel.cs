using System;
using Prism.Navigation;
using Prism.Services;
using TaxCalculator.Models;
using TaxCalculator.Services;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace TaxCalculator.ViewModels
{

    public class TaxCalculationPageViewModel : ViewModelBase
    {
        protected ITaxService TaxService { get; }

        private DataFromUI _uiData;
        public DataFromUI UIData
        {
            get => _uiData;
            set => this.RaiseAndSetIfChanged(ref _uiData, value);
        }

        private bool _canExecuteCalculateTax;
        public bool  CanExecuteCalculateTax
        {
            get => _canExecuteCalculateTax;
            set => this.RaiseAndSetIfChanged(ref _canExecuteCalculateTax, value);
        }

        public ReactiveCommand<Unit, Unit> GetRatesCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CalculateTaxCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ValidateCalculateTaxCommand { get; set; }

        public TaxCalculationPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ITaxService taxService) : base(navigationService, pageDialogService)
        {
            //Injected TaxService for use
            TaxService = taxService;

            //We only have TaxJar client atm so creating this here.
            UIData = new DataFromUI(TaxServices.taxJar);

            //Enable/Disable Rates button using ReactiveUI as long as it has required data.
            //This can be expanded quite a bit to include other UI rules
            //Rates must have ZIP

            CalculateTaxCommand = ReactiveCommand.CreateFromTask(CalculateTaxCommandExecuted);


            ValidateCalculateTaxCommand = ReactiveCommand.CreateFromTask(ValidateCalculateTaxCommandExecuted);

            SetupUIRules();
        }

        public void SetupUIRules()
        {
            switch (UIData.CalculatorType)
            {
                case TaxServices.taxJar:
                    {
                        var canExecuteRates = this.WhenAnyValue(x => x.UIData.SourceAddress.Zip, x => x.UIData.SourceAddress.Zip, (zip, zip2) => zip != null && zip.Length >= 3);
                        GetRatesCommand = ReactiveCommand.CreateFromTask(GetRatesCommandExecuted, canExecuteRates);

                        this.WhenAnyValue(x => x.UIData.DestinationAddress.Country).Subscribe(country =>
                        {
                            UIData.DestinationAddress.IsZipRequired = country.ToUpper() == "UNITED STATES" ? true : false;
                            UIData.DestinationAddress.IsStateRequired = (country.ToUpper() == "UNITED STATES" || country.ToUpper() == "CANADA") ? true : false;
                        });

                        this.WhenAnyValue(x => x.UIData.DestinationAddress.Street, x => x.UIData.DestinationAddress.City, x => x.UIData.DestinationAddress.State, x => x.UIData.DestinationAddress.Zip, x => x.UIData.DestinationAddress.Country, x => x.UIData.Shipping).Subscribe(data =>
                        {
                            ValidateCalculateTaxCommand.Execute();
                        });

                        break;
                    }
                default:
                    break;
            }

        }

        private async Task ValidateCalculateTaxCommandExecuted()
        {
            await Task.Delay(50);

            switch (UIData.CalculatorType)
            {
                case TaxServices.taxJar:
                    {
                        var hasNeededInformation = true;
                        var hasShippingAndCountry = UIData.Shipping != null && UIData.Amount != null && !string.IsNullOrEmpty(UIData.DestinationAddress.Country);
                        var requiresZip = UIData.DestinationAddress.IsZipRequired;
                        var hasZip = !string.IsNullOrEmpty(UIData.DestinationAddress.Zip);
                        var requiresState = UIData.DestinationAddress.IsStateRequired;
                        var hasState = !string.IsNullOrEmpty(UIData.DestinationAddress.State);

                        if (requiresZip && requiresState)
                        {
                            hasNeededInformation = hasZip && hasState;

                        } else if (requiresZip)
                        {
                            hasNeededInformation = hasZip;
                        }
                        else if (requiresState)
                        {
                            hasNeededInformation = hasState;
                        }

                        CanExecuteCalculateTax = hasShippingAndCountry && hasNeededInformation;

                        break;
                    }
                default:
                    break;
            }
        }

        public async Task CalculateTaxCommandExecuted()
        {
            try
            {
                var taxes = await TaxService.CalculateTax(UIData);

                //Showing response, normally I would handle this data in a model and display in a view or added to the existing view depending on requirements.
                await PageDialogService.DisplayAlertAsync("Taxes", taxes, "OK");
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("ERROR", ex.Message, "OK");
            }
        }

        public async Task GetRatesCommandExecuted()
        {
            try
            {
                var rate = await TaxService.GetTaxRate(UIData);

                //Showing response, normally I would handle this data in a model and display in a view or added to the existing view depending on requirements.
                await PageDialogService.DisplayAlertAsync("Rates", rate, "OK");
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("ERROR", ex.Message, "OK");
            }
        }

        //Access to underlying navigation if needed
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Console.WriteLine("OnNavigatedTo");
        }
    }
}

