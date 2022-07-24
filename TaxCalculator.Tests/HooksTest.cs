using System;
using NUnit.Framework;
using Xamarin.Forms;

namespace TaxCalculator.Tests
{
    public class HooksTest
    {
        public static TCTestApp App { get; private set; }

        [SetUp]
        public static void Setup()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            App = new TCTestApp();
        }

        [Test]
        public static void IsAppRunning()
        {
            //Is the app running
            Assert.NotNull(App);
        }
    }
}

