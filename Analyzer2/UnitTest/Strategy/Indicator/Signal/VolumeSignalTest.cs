using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    [TestFixture]
    public class VolumeSignalTest
    {
        [Test]
        public void Run()
        {
            VolumeSignal vs = new VolumeSignal(0.3, 0.6);

            Assert.IsFalse(vs.AddStock(new StockData { Amount = 100.0 }));
            Assert.IsFalse(vs.AddStock(new StockData { Amount = 100.0 }));
            Assert.IsFalse(vs.AddStock(new StockData { Amount = 100.0 }));

            Assert.IsTrue(vs.AddStock(new StockData { Amount = 69 }));

            Assert.AreEqual(vs.GetSignal(), OperType.Buy);

            Assert.IsTrue(vs.AddStock(new StockData { Amount = 100.0 }));
            Assert.IsTrue(vs.AddStock(new StockData { Amount = 100.0 }));
            Assert.IsTrue(vs.AddStock(new StockData { Amount = 100.0 }));

            Assert.IsTrue(vs.AddStock(new StockData { Amount = 161 }));

            Assert.AreEqual(vs.GetSignal(), OperType.Sell);
        }
    }
}
