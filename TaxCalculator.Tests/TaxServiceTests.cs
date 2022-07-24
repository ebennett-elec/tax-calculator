using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Prism.DryIoc;
using Prism.Ioc;
using TaxCalculator.Models;
using TaxCalculator.Services;
using TaxCalculator.ViewModels;
using TaxCalculator.Views;
using Xamarin.Forms;

namespace TaxCalculator.Tests
{
    [TestFixture]
    public class TaxServiceTests : Base
    {
        private ITaxService TaxService;

        [OneTimeSetUp]
        public void SetupFixture()
        {
            TaxService = App.Container.Resolve<ITaxService>();
        }

        [Test]
        public void TaxServiceValid()
        {
            Assert.IsNotNull(TaxService);
        }

        [Test]
        public void VerifyDataFromUITest()
        {
            //Verify DataFromUI object
            DataFromUI data = new DataFromUI(TaxServices.taxJar);
            Assert.AreEqual(data.CalculatorType, TaxServices.taxJar);
            Assert.AreEqual(data.Shipping, 0.0);
            Assert.AreEqual(data.Amount, 0.0);
        }

        [Test]
        public void TestTaxServiceCalculateTaxException()
        {
            //Should throw execption with invalid taxClient
            Assert.ThrowsAsync<Exception>(() => TaxService.CalculateTax(new DataFromUI()));
        }

        [Test]
        public void TestTaxServiceGetRatesException()
        {
            //Should throw execption with invalid taxClient
            Assert.ThrowsAsync<Exception>(() => TaxService.GetTaxRate(new DataFromUI()));
        }

        [Test]
        public async Task TestTaxServiceGetRatesAsync()
        {
            //Our mock should return a valid display string (Ideally this would be returning the formed object and not just a string)
            Assert.IsNotNull(await TaxService.GetTaxRate(new DataFromUI(TaxServices.taxJar)));
        }

        [Test]
        public async Task TestTaxServiceCalculateTax()
        {
            //Our mock should return a valid display string (Ideally this would be returning the formed object and not just a string)
            Assert.IsNotNull(await TaxService.CalculateTax(new DataFromUI(TaxServices.taxJar)));
        }
    }
}