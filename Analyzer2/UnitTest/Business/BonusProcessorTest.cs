using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using NUnit.Framework;

namespace FinanceAnalyzer.Business
{
    [TestFixture]
    public class BonusProcessorTest
    {
        [Test]
        public void Run()
        {
            IBonusReader reader = new FakeBonusReader();

            BonusProcessor processor = new BonusProcessor();
            processor.Load(FakeBonusReader.STOCKID, reader);

            Assert.IsTrue(processor.IsBonusListOnDate(new DateTime(2009, 1, 4)));
            Assert.IsTrue(processor.IsDividendDate(new DateTime(2009, 1, 3)));
            Assert.IsTrue(processor.IsExexDividendDate(new DateTime(2009, 1, 2)));

            Assert.IsNotNull(processor.FindBonus(new DateTime(2009, 1, 2)));
            Assert.IsNull(processor.FindBonus(new DateTime(2009, 1, 1)));
        }
    }
}
