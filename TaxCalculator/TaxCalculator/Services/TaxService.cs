using System;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public class TaxService : ITaxService
    {
        protected ITaxJarClient TaxJarClient { get; }

        public TaxService(ITaxJarClient taxJarClient)
        {
            TaxJarClient = taxJarClient;
        }

        public async Task<string> CalculateTax(DataFromUI data)
        {
            switch (data.CalculatorType)
            {
                case TaxServices.taxJar:
                {
                        return await TaxJarClient.CalculateTax(data);
                    }
                default:
                    break;
            }

            throw new Exception("TaxClient not configured");
        }

        public async Task<string> GetTaxRate(DataFromUI data)
        {
            switch (data.CalculatorType)
            {
                case TaxServices.taxJar:
                    {
                        return await TaxJarClient.GetTaxRates(data);
                    }
                default:
                    return "Unknown Tax Client";
            }
        }
    }
}


