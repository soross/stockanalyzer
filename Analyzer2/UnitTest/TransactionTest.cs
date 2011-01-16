using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void TestBuyStock()
        {
            int stockCount = Transaction.GetCanBuyStockCount(5000, 10);

            Assert.AreEqual(stockCount, 400);
        }

        [Test]
        public void TestBuyStock2()
        {
            int stockCount = Transaction.GetCanBuyStockCount(10000, 10);

            Assert.AreEqual(stockCount, 900);
        }

        [Test]
        public void TestGetTotalCharge()
        {
            double val = Transaction.GetTotalCharge(1000);

            Assert.Greater(val, 1000);
        }
    }
}
