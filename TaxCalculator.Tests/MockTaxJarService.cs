using System;
using System.Threading.Tasks;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Tests
{
    public class MockTaxJarService : ITaxJarClient
    {
        public Task<string> CalculateTax(DataFromUI data)
        {
            return Task.FromResult("Tax returned");
        }

        public Task<string> GetTaxRates(DataFromUI data)
        {
            return Task.FromResult("Rates returned");
        }
    }

}

