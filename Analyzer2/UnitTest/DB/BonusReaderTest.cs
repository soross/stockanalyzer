using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace FinanceAnalyzer.DB
{
    [TestFixture]
    public class BonusReaderTest
    {
        [Test]
        public void Run()
        {
            BonusReader reader = new BonusReader();
            reader.LoadAll();

            Assert.IsTrue(reader.Count() > 0);
        }        
    }
}
