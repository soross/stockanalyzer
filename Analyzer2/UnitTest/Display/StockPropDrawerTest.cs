using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FinanceAnalyzer.Display
{
    [TestFixture]
    public class StockPropDrawerTest
    {
        [Test]
        public void TestDate()
        {
            _Drawer.MaxDate = new DateTime(2009, 1, 1);
            Assert.AreEqual(_Drawer.MaxDate, new DateTime(2009, 1, 1));

            _Drawer.MinDate = new DateTime(2008, 1, 1);
            Assert.AreEqual(_Drawer.MinDate, new DateTime(2008, 1, 1));
        }

        [Test]
        public void TestGet()
        {
            _Drawer.AddDayStock(new DateTime(2009, 10, 13), 
                FakeStockDataCreator.Create(new DateTime(2009, 10, 13), 102, 106, 101, 105, 1000));

            Assert.IsNotNull(_Drawer.GetAt(new DateTime(2009, 10, 13)));
            Assert.IsNull(_Drawer.GetAt(new DateTime(2009, 10, 14)));
        }

        StockPropDrawer _Drawer = new StockPropDrawer();
    }
}
