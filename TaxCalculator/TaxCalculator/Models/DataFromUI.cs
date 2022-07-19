using System;
using Prism.Mvvm;
using ReactiveUI;

namespace TaxCalculator.Models
{
    public enum TaxServices
    {
        taxJar = 1
    }

    public class DataFromUI : BindableBase
    {
        public TaxServices CalculatorType { get; set; }

        private AddressDataModel _sourceAddress;
        public AddressDataModel SourceAddress
        {
            get => _sourceAddress;
            set { this.SetProperty(ref _sourceAddress, value); }
        }

        private AddressDataModel _destinationAddress;
        public AddressDataModel DestinationAddress
        {
            get => _destinationAddress;
            set { this.SetProperty(ref _destinationAddress, value); }
        }

        private double? _shipping;
        public double? Shipping
        {
            get => _shipping;
            set { this.SetProperty(ref _shipping, value); }
        }

        private double? _amount;
        public double? Amount
        {
            get => _amount;
            set { this.SetProperty(ref _amount, value); }
        }

        public DataFromUI(TaxServices calculatorType)
        {
            CalculatorType = calculatorType;
            SourceAddress = new AddressDataModel();
            DestinationAddress = new AddressDataModel();

            Amount = 0.0;
            Shipping = 0.0;
        }
    }
}

