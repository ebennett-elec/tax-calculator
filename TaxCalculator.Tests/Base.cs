using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.Forms;

namespace TaxCalculator.Tests
{
    public class Base
    {
        public static TCTestApp App { get; private set; }

        [SetUp]
        public void Setup()
        {
            HooksTest.Setup();
            App = HooksTest.App;
        }
    }
}

