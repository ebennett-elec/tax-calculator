using System;
using System.IO;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxService
    {
        Task<string> GetTaxRate(DataFromUI data);
        Task<string> CalculateTax(DataFromUI data);
    }
}

