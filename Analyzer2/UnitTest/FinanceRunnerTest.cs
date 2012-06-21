using FinanceAnalyzer.Business;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Impl;
using FinanceAnalyzer.Strategy.Judger;
using FinanceAnalyzer.Strategy.Result;
using NUnit.Framework;

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
