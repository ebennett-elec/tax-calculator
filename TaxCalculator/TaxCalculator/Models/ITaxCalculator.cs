using System;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public interface ITaxCalculator
    {
        Task<string> CalculateTax();
        Task<string> GetTaxRate();
    }
}

