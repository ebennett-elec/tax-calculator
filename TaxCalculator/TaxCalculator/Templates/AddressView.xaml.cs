using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using System.Text.Json;
using System.Reflection;
using TaxCalculator.Models;

namespace TaxCalculator.Templates
{
    public partial class AddressView : ContentView
    {

        public static readonly BindableProperty StreetProperty = BindableProperty.Create(nameof(Street),
                            typeof(string),
                            typeof(AddressView),
                            defaultValue: string.Empty,
                            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CityProperty = BindableProperty.Create(nameof(City),
                            typeof(string),
                            typeof(AddressView),
                            defaultValue: string.Empty,
                            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State),
                            typeof(string),
                            typeof(AddressView),
                            defaultValue: string.Empty,
                            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ZipProperty = BindableProperty.Create(nameof(Zip),
                            typeof(string),
                            typeof(AddressView),
                            defaultValue: string.Empty,
                            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CountryProperty = BindableProperty.Create(nameof(Country),
                            typeof(string),
                            typeof(AddressView),
                            defaultValue: string.Empty,
                            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IsStateRequiredProperty = BindableProperty.Create(nameof(IsStateRequired),
                            typeof(bool),
                            typeof(AddressView),
                            defaultValue: false,
                            defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty IsZipRequiredProperty = BindableProperty.Create(nameof(IsZipRequired),
                            typeof(bool),
                            typeof(AddressView),
                            defaultValue: false,
                            defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty IsCountryRequiredProperty = BindableProperty.Create(nameof(IsCountryRequired),
                            typeof(bool),
                            typeof(AddressView),
                            defaultValue: false,
                            defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty SelectedCountryAbreviationProperty = BindableProperty.Create(nameof(SelectedCountryAbreviation),
                            typeof(string),
                            typeof(AddressView),
                            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty SelectedStateAbreviationProperty = BindableProperty.Create(nameof(SelectedStateAbreviation),
                            typeof(string),
                            typeof(AddressView),
                            defaultBindingMode: BindingMode.TwoWay);


        private List<CountriesModel> isoCountriesList;
        private Dictionary<string, string> isoStatesList;
        private Dictionary<string, string> isoCanadaProvinceList;

        public string Street
        {
            get => (string)GetValue(StreetProperty);
            set => SetValue(StreetProperty, value);
        }

        public string City
        {
            get => (string)GetValue(CityProperty);
            set => SetValue(CityProperty, value);
        }

        public string State
        {
            get => (string)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public string SelectedStateAbreviation
        {
            get => (string)GetValue(SelectedStateAbreviationProperty);
            set => SetValue(SelectedStateAbreviationProperty, value);
        }

        public string Zip
        {
            get => (string)GetValue(ZipProperty);
            set => SetValue(ZipProperty, value);
        }

        public string Country
        {
            get => (string)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }

        public string SelectedCountryAbreviation
        {
            get => (string)GetValue(SelectedCountryAbreviationProperty);
            set => SetValue(SelectedCountryAbreviationProperty, value);
        }

        public bool IsStateRequired
        {
            get => (bool)GetValue(IsStateRequiredProperty);
            set => SetValue(IsStateRequiredProperty, value);
        }

        public bool IsZipRequired
        {
            get => (bool)GetValue(IsZipRequiredProperty);
            set => SetValue(IsZipRequiredProperty, value);
        }

        public bool IsCountryRequired
        {
            get => (bool)GetValue(IsCountryRequiredProperty);
            set => SetValue(IsCountryRequiredProperty, value);
        }

        public AddressView()
        {
            InitializeComponent();
            countryPicker.SelectedIndexChanged += countryPickerIndexChanged;
            statePicker.SelectedIndexChanged += statePickerIndexChanged;
            PopulatePickerLists();
        }

        private void countryPickerIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            var index = picker.SelectedIndex;
            if (index > 0 && index <= isoCountriesList.Count)
            {
                var country = isoCountriesList[index];
                if (country == null) return;

                SelectedCountryAbreviation = country.Code;
            }

            var stateProvinceList = new List<string>();

            if (SelectedCountryAbreviation == "CA")
            {
                //Canada
                stateProvinceList.AddRange(new List<string>(isoCanadaProvinceList.Keys));
            } else
            {
                //US
                stateProvinceList.AddRange(new List<string>(isoStatesList.Keys));
            }

            statePicker.ItemsSource = stateProvinceList;
        }

        private void statePickerIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            var stateKey = (string)picker.SelectedItem;
            if (string.IsNullOrEmpty(stateKey)) return;

            var currentCountry = (string)countryPicker.SelectedItem;

            if (currentCountry.ToUpper() == "UNITED STATES")
            {
                var abbreviation = isoStatesList[stateKey];
                if (string.IsNullOrEmpty(abbreviation)) return;
                SelectedStateAbreviation = abbreviation;
            } else
            {
                var abbreviation = isoCanadaProvinceList[stateKey];
                if (string.IsNullOrEmpty(abbreviation)) return;
                SelectedStateAbreviation = abbreviation;
            }
        }

        public void PopulatePickerLists()
        {
            var assembly = typeof(AddressView).GetTypeInfo().Assembly;

            //Load State ISO list from local file
            string isoStates;
            string jsonFileName = "JSON.ISOStates.json";
            
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                isoStates = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(isoStates))
            {
                isoStatesList = JsonSerializer.Deserialize<Dictionary<string, string>>(isoStates);

                var statesList = new List<string>();
                statesList.AddRange(new List<string>(isoStatesList.Keys));
                statePicker.ItemsSource = statesList;
            }


            jsonFileName = "JSON.ISOCanadaProvinces.json";
            string isoCanadaStates;

            stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                isoCanadaStates = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(isoCanadaStates))
            {
                isoCanadaProvinceList = JsonSerializer.Deserialize<Dictionary<string, string>>(isoCanadaStates);
            }

            //Load Country ISO list from local file
            string isoCountries;
            jsonFileName = "JSON.ISOCountries.json";

            stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                isoCountries = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(isoCountries))
            {
                isoCountriesList = JsonSerializer.Deserialize<List<CountriesModel>>(isoCountries);
                var countryList = new List<string>();
                foreach(var country in isoCountriesList)
                {
                    countryList.Add(country.Name);
                }
                countryPicker.ItemsSource = countryList;
            }
        }
    }
}

