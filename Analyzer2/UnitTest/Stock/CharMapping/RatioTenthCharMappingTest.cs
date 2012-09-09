using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock.CharMapping
{
    [TestFixture]
    class RatioTenthCharMappingTest
    {
        [Test]
        public void ParseRatioTest()
        {
            StockHistoryCharMapping mapping = new RatioTenthCharMapping(1);

            SimpleStockHistory history = new SimpleStockHistory();

            string str = mapping.GetCharMapping(history);

            Assert.IsTrue(str == "kubk|kmkm|jnjj|");
        }

        [Test]
        public void ParseCharsTest()
        {
            StockHistoryCharMapping mapping = new RatioTenthCharMapping(1);

            DateTime now = DateTime.Now;
            StockData sd1 = FakeStockDataCreator.Create(now.AddDays(-1), 10, 10.8, 9.7, 10, 30);
            StockData sd2 = FakeStockDataCreator.Create(now, 11, 11.2, 10.6, 10.8, 50);

            string result = mapping.ParseChars(sd1, sd2);
            Assert.IsTrue(result == "ulhj");
        }

        [Test]
        public void SingleCharTest()
        {
            RatioTenthCharMapping mapping = new RatioTenthCharMapping(1);

            string s = mapping.GetRatioString(0.01);            
            Assert.AreEqual(s, "a");

            s = mapping.GetRatioString(0.0);
            Assert.AreEqual(s, "A");

            s = mapping.GetRatioString(-0.01);
            Assert.AreEqual(s, "B");

            s = mapping.GetRatioString(-0.02);
            Assert.AreEqual(s, "C");

            s = mapping.GetRatioString(-0.029);
            Assert.AreEqual(s, "D");

            s = mapping.GetRatioString(0.02);
            Assert.AreEqual(s, "b");
            s = mapping.GetRatioString(0.021);
            Assert.AreEqual(s, "b");

            s = mapping.GetRatioString(0.029);
            Assert.AreEqual(s, "c");

            s = mapping.GetRatioString(0.03);
            Assert.AreEqual(s, "c");
        }
    }
}
