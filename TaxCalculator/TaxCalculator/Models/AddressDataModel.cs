using System;
using Prism.Mvvm;
using ReactiveUI;

namespace TaxCalculator.Models
{
    public class AddressDataModel : BindableBase
    {

        public AddressDataModel()
        {
            //Setting as default country
            Country = "United States";
            SelectedCountryAbreviation = "US";
        }

        private string _street;
        public string Street
        {
            get => _street;
            set { this.SetProperty(ref _street, value); }
        }

        private string _city;
        public string City
        {
            get => _city;
            set { this.SetProperty(ref _city, value); }
        }

        private string _state;
        public string State
        {
            get => _state;
            set { this.SetProperty(ref _state, value); }
        }

        private string _zip;
        public string Zip
        {
            get => _zip;
            set { this.SetProperty(ref _zip, value); }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set { this.SetProperty(ref _country, value); }
        }

        private string _selectedCountryAbreviation;
        public string SelectedCountryAbreviation
        {
            get => _selectedCountryAbreviation;
            set { this.SetProperty(ref _selectedCountryAbreviation, value); }
        }

        private string _selectedStateAbreviation;
        public string SelectedStateAbreviation
        {
            get => _selectedStateAbreviation;
            set { this.SetProperty(ref _selectedStateAbreviation, value); }
        }

        private bool _isZipRequired;
        public bool IsZipRequired
        {
            get => _isZipRequired;
            set { this.SetProperty(ref _isZipRequired, value); }
        }

        private bool _isStateRequired;
        public bool IsStateRequired
        {
            get => _isStateRequired;
            set { this.SetProperty(ref _isStateRequired, value); }
        }

    }
}

