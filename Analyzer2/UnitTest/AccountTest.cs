using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class AccountTest
    {
        [SetUp]
        public void Init()
        {
            LogMgr.Logger = new DummyLog();
        }

        [Test]
        public void Test()
        {
            Account acc = new Account();
            acc.BankRoll = 50000;

            IStockHolder holder = new StockHolder();
            acc.Holder = holder;

            IBonusReader reader = new FakeBonusReader();

            BonusProcessor processor = new BonusProcessor();
            processor.Load(FakeBonusReader.STOCKID, reader);

            acc.Processor = processor;

            acc.DoBusiness(new StockOper(UNITPRICE, BUYCOUNT, OperType.Buy));

            Assert.IsTrue(acc.Holder.StockCount() == BUYCOUNT);
            Assert.IsTrue(acc.Holder.UnitPrice >= UNITPRICE);

            acc.ProcessBonus(new DateTime(2009,1,1));
            acc.ProcessBonus(new DateTime(2009, 1, 2));

            double cash = acc.BankRoll;
            acc.ProcessBonus(new DateTime(2009, 1, 3));
            Assert.IsTrue(acc.Holder.StockCount() == BUYCOUNT);
            Assert.IsTrue(acc.BankRoll > cash);
            acc.ProcessBonus(new DateTime(2009, 1, 4));
            Assert.IsTrue(acc.Holder.StockCount() == BUYCOUNT + (BUYCOUNT * 0.1));
        }

        [Test]
        public void DoBusiness()
        {
            Account acc = new Account();
            acc.BankRoll = 50000;

            Assert.IsFalse(acc.DoBusiness(null));
        }

        private const double UNITPRICE = 5.25;
        private const int BUYCOUNT = 6000;
    }
}
