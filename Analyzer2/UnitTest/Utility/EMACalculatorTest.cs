using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FinanceAnalyzer.Utility
{
    [TestFixture]
    public class EMACalculatorTest
    {
        [Test]
        public void Run()
        {
            List<double> arr = new List<double>();
            for (int i = 1; i < 10; i++ )
            {
                arr.Add(i);
            }

            List<double> results = EmaCalculator.Calc(arr);

            Assert.IsTrue(ComparePrice(results[0], 1));
            Assert.IsTrue(ComparePrice(results[1], 1.2));
            Assert.IsTrue(ComparePrice(results[2], 1.56));
            Assert.IsTrue(ComparePrice(results[3], 2.048));
        }

        private bool ComparePrice(double price1, double price2)
        {
            return Math.Abs(price1 - price2) <= 0.0001;
        }
    }
}
