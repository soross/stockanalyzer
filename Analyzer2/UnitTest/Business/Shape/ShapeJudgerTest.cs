using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business.Shape
{
    [TestFixture]
    class ShapeJudgerTest
    {
        [Test]
        public void IsReverseT_Test()
        {
            StockData sd2 = FakeStockDataCreator.Create(DateTime.Now, 10, 10.4, 9.96, 10, 50);

            Assert.IsTrue(ShapeJudger.IsReverseT(sd2));
        }
    }
}
