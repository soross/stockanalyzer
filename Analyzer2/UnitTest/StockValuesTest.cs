using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Log;
using NUnit.Framework;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class StockValuesTest
    {
        [SetUp]
        public void Init()
        {
            LogMgr.Logger = new DummyLog();
        }

        [Test]
        public void SetTotalValue()
        {
            IStockValues values = new StockValues();

            values.SetTotalValue(new DateTime(2009, 3, 23), 10.0);
            Assert.AreEqual(values.GetAllDate().Count, 1);

            values.SetTotalValue(new DateTime(2009, 3, 24), 12.0);
            Assert.AreEqual(values.GetAllDate().Count, 2);

            values.SetTotalValue(new DateTime(2009, 3, 25), -1); // invalid
            Assert.AreEqual(values.GetAllDate().Count, 2);
        }

        [Test]
        public void GetTotalValue()
        {
            IStockValues values = new StockValues();

            DateTime dt = new DateTime(2009, 3, 23);
            values.SetTotalValue(dt, 10.0);

            double val = values.GetTotalValue(dt);
            Assert.AreEqual(val, 10.0);

            val = values.GetTotalValue(new DateTime(1980, 1, 1));
            Assert.IsTrue(val < 0);
        }
    }
}
