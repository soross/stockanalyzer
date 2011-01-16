using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Strategy.Impl;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class FinanceRunnerTest
    {
        [Test]
        public void Go()
        {
            FakeStockHistory hist = new FakeStockHistory();
            hist.Init();

            LogMgr.Logger = new DummyLog();

            FinanceRunner runner = new FinanceRunner();
            runner.CurrentBonusProcessor = new FakeIBonusProcessor();
            StrategyFactory factory = new StrategyFactory();
            factory.Init();
            runner.Go(hist, factory);

            StrategyResults results = runner.Results;
            Assert.IsTrue(results.AllStrategyNames.Count > 0);

            IFinanceStrategy strategy = new StrategyThreeDay(new UpJudger());
            IStockValues values = results.GetResult(strategy.Name);

            Assert.IsNotNull(values);

            LogMgr.Logger.Close();
        }
    }
}
