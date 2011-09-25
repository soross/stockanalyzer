using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer;
using NUnit.Framework;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class StrategyResultsTest
    {
        [Test]
        public void AddResult()
        {
            StrategyResults res = new StrategyResults();
            res.AddResult("Test", new StockValues());

            ICollection<string> names = res.AllStrategyNames;
            Assert.AreEqual(names.Count, 1);
        }

        [Test]
        public void GetResult()
        {
            StrategyResults res = new StrategyResults();
            res.AddResult("Test", new StockValues());

            IStockValues val = res.GetResult("Test");
            Assert.IsNotNull(val);

            val = res.GetResult("NOTEXIST");
            Assert.IsNull(val);
        }
    }
}
