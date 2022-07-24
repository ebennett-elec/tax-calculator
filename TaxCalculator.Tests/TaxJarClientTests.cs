using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Prism.Ioc;
using TaxCalculator.Models;
using TaxCalculator.Services;
using TaxCalculator.ViewModels;

namespace TaxCalculator.Tests
{
    [TestFixture]
    public class TaxJarClientTests : Base
    {
        [Test]
        public void VerifyTaxJarClientTest()
        {
            var taxService = App.Container.Resolve<ITaxJarClient>();
            Assert.IsNotNull(taxService);
        }
    }
}

