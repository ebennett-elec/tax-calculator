using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Prism.Ioc;
using TaxCalculator.ViewModels;

namespace TaxCalculator.Tests
{
    [TestFixture]
    public class TaxCalculationPageTests : Base
    {
        private TaxCalculationPageViewModel viewModel;

        [SetUp]
        public void SetupFixture()
        {
            viewModel = App.Container.Resolve<TaxCalculationPageViewModel>();
        }

        [Test]
        public void VerifyTaxCalculationPageVMTest()
        {
            Assert.IsNotNull(viewModel);
        }

        [Test]
        public void UIDataAmountTest()
        {
            var uiDataAmount = viewModel.UIData.Amount;
            Assert.IsNotNull(uiDataAmount);

            uiDataAmount += 10;
            Assert.AreEqual(uiDataAmount, 10.0);
        }

        [Test]
        public void UIDataShippingTest()
        {
            var uiDataShipping = viewModel.UIData.Shipping;
            Assert.IsNotNull(uiDataShipping);

            uiDataShipping += 10;
            Assert.AreEqual(uiDataShipping, 10.0);
        }

        [Test]
        public void UIDataNotNullTest()
        {
            var uiData = viewModel.UIData;
            Assert.IsNotNull(uiData);

            var uiDataShipping = uiData.Shipping;
            Assert.IsNotNull(uiDataShipping);

            var uiDataAmount = uiData.Amount;
            Assert.IsNotNull(uiDataAmount);

        }

        [Test]
        public void UIDataCalculateTaxCanExecuteTest()
        {
            //false with default values
            var canExecute = viewModel.CanExecuteCalculateTax;
            Assert.IsFalse(canExecute);
        }
    }
}

