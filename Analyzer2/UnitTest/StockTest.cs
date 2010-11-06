using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer;
using NUnit.Framework;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class StockTest
    {
        [Test]
        public void TestBuy()
        {
            Stock val = new Stock();
            val.Buy(100, 1.0);
            Assert.AreEqual(val.TotalPrice, 100.0);
            Assert.AreEqual(val.Count, 100);
        }

        [Test]
        public void TestCtor()
        {
            Stock val = new Stock();
            Assert.IsTrue(val.UnitPrice == 0);
            Assert.IsTrue(val.Count == 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSell()
        {
            Stock val = new Stock();
            val.Buy(100, 1.0);
            val.Sell(200, 1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSellInvalid()
        {
            Stock val = new Stock();
            val.Buy(100, 1.0);
            val.Sell(100, -1);
        }

        [Test]
        public void TestSell2()
        {
            Stock val = new Stock();
            val.Buy(100, 1.0);
            val.Sell(100, 1.1);

            Assert.IsTrue(val.Count == 0);
            Assert.IsTrue(val.UnitPrice == 0);
        }

        [Test]
        public void TestSell3()
        {
            Stock val = new Stock();

            val.Buy(100, 1.0);
            double amount = val.Sell(50, 1.5);

            Assert.AreEqual(val.UnitPrice, 0.5);
            Assert.AreEqual(val.Count, 50);
            Assert.AreEqual(amount, 75);
        }
    }
}
