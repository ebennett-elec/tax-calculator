using System;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxClient
    {
        Task<string> GetTaxRates(DataFromUI data);
        Task<string> CalculateTax(DataFromUI data);
    }
}

