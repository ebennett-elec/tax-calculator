using System;
using System.Threading.Tasks;
using TaxCalculator.Helpers;
using RestSharp;
using TaxCalculator.Models;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Net;

namespace TaxCalculator.Services
{
    public class TaxJarClient : ITaxJarClient
    {
        readonly RestClient _client;

        public TaxJarClient()
        {
            var options = new RestClientOptions(Settings.TaxJarServiceURI + "v2");
            _client = new RestClient(options).AddDefaultHeader("Authorization", "Token token=" + Settings.TaxJarServiceApiKey);
        }

        public async Task<string> CalculateTax(DataFromUI data)
        {
            var request = new RestRequest("taxes");

            var tax = new TaxJarTaxRequest()
            {
                from_country = data.SourceAddress.SelectedCountryAbreviation,
                from_zip = data.SourceAddress.Zip,
                from_state = data.SourceAddress.SelectedStateAbreviation,
                to_country = data.DestinationAddress.SelectedCountryAbreviation,
                to_state = data.DestinationAddress.SelectedStateAbreviation,
                to_zip = data.DestinationAddress.Zip,
                amount = data.Amount ?? 0.0,
                shipping = data.Shipping ?? 0.0
            };

            var json = JsonSerializer.Serialize(tax, new JsonSerializerOptions() { WriteIndented = true });

            request.AddJsonBody(json);
        
            var response = await _client.PostAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var taxes = JsonSerializer.Deserialize<TaxJarTax>(response.Content);

                //I Deserialized for use but for this test app I am just serializing it back and showing pretty JSON text in a popup response.
                var displayText = JsonSerializer.Serialize(taxes, new JsonSerializerOptions() { WriteIndented = true });

                return displayText;
            }

            throw new Exception("Unable to resolve data\nStatus Code: " + response.StatusCode);
        }

        public async Task<string> GetTaxRates(DataFromUI data)
        {
            var request = new RestRequest("rates/{zip}")
                .AddUrlSegment("zip", data.SourceAddress.Zip);

            if (!string.IsNullOrEmpty(data.SourceAddress.Country))
            {
                request.AddQueryParameter("country", data.SourceAddress.SelectedCountryAbreviation);
            }

            if (!string.IsNullOrEmpty(data.SourceAddress.State))
            {
                //Only include state if US
                if (data.SourceAddress.State.ToUpper() == "UNITED STATES")
                {
                    request.AddQueryParameter("state", data.SourceAddress.SelectedStateAbreviation);
                }
            }

            if (!string.IsNullOrEmpty(data.SourceAddress.City))
            {
                request.AddQueryParameter("city", data.SourceAddress.City);
            }

            if (!string.IsNullOrEmpty(data.SourceAddress.City))
            {
                request.AddQueryParameter("street", data.SourceAddress.Street);
            }

            var response = await _client.GetAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var rates = JsonSerializer.Deserialize<TaxJarRate>(response.Content);

                //I Deserialized for use but for this test app I am just serializing it back and showing pretty JSON text in a popup response.
                var displayText = JsonSerializer.Serialize(rates, new JsonSerializerOptions() { WriteIndented = true });

                return displayText;
            }

            throw new Exception("Unable to resolve data\nStatus Code: " + response.StatusCode);
        }
    }
}

