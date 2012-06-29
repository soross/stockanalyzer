using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    [TestFixture]
    class EngulfingSignalTest
    {
        [Test]
        public void RunBuy()
        {
            EngulfingSignal es = new EngulfingSignal();

            StockData sd1 = new StockData { StartPrice = 102, MaxPrice = 103, MinPrice = 98, EndPrice = 98 };
            Assert.IsFalse(es.AddStock(sd1));
            StockData sd2 = new StockData { StartPrice = 97, MaxPrice = 105, MinPrice = 96, EndPrice = 105 };
            Assert.IsTrue(es.AddStock(sd2));

            Assert.AreEqual(es.GetSignal(), OperType.Buy);
        }

        [Test]
        public void RunBuy2()
        {
            EngulfingSignal es = new EngulfingSignal();

            StockData sd1 = new StockData { StartPrice = 102, MaxPrice = 102, MinPrice = 98, EndPrice = 98 };
            Assert.IsFalse(es.AddStock(sd1));
            StockData sd2 = new StockData { StartPrice = 97, MaxPrice = 105, MinPrice = 97, EndPrice = 105 };
            Assert.IsTrue(es.AddStock(sd2));

            Assert.AreEqual(es.GetSignal(), OperType.Buy);
        }

        [Test]
        public void RunNoOper()
        {
            EngulfingSignal es = new EngulfingSignal();

            StockData sd1 = new StockData { StartPrice = 102, MaxPrice = 102, MinPrice = 98, EndPrice = 98 };
            StockData sd2 = new StockData { StartPrice = 97, MaxPrice = 105, MinPrice = 97, EndPrice = 105 };
            Assert.IsFalse(es.AddStock(sd2));            
            Assert.IsTrue(es.AddStock(sd1));

            Assert.AreEqual(es.GetSignal(), OperType.NoOper);
        }

        [Test]
        public void RunSell()
        {
            EngulfingSignal es = new EngulfingSignal();

            StockData sd1 = new StockData { StartPrice = 106, MaxPrice = 106, MinPrice = 96, EndPrice = 96 };
            StockData sd2 = new StockData { StartPrice = 97, MaxPrice = 105, MinPrice = 97, EndPrice = 105 };
            Assert.IsFalse(es.AddStock(sd2));
            Assert.IsTrue(es.AddStock(sd1));

            Assert.AreEqual(es.GetSignal(), OperType.Sell);
        }

        [Test]
        public void RunNoOper2()
        {
            EngulfingSignal es = new EngulfingSignal();

            StockData sd1 = new StockData { StartPrice = 102, MaxPrice = 102, MinPrice = 98, EndPrice = 98 };
            StockData sd2 = new StockData { StartPrice = 99, MaxPrice = 105, MinPrice = 99, EndPrice = 105 };
            Assert.IsFalse(es.AddStock(sd2));
            Assert.IsTrue(es.AddStock(sd1));

            Assert.AreEqual(es.GetSignal(), OperType.NoOper);
        }
    }
}
