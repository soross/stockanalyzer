using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

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

            Assert.IsTrue(str == "");
        }
    }
}
