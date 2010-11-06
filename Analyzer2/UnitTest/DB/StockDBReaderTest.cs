using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FinanceAnalyzer.DB
{
    [TestFixture]
    public class StockDBReaderTest
    {
        [Test]
        public void TestSelectAll()
        {
            StockDBReader reader = new StockDBReader();
            reader.LoadAll();

            Assert.IsTrue(reader.ItemCount > 0);
        }

        [Test]
        public void TestSelectAllId()
        {
            IList<int> arr = StockDBReader.LoadId();
            Assert.IsTrue(arr.Count > 0);
        }

    }
}
