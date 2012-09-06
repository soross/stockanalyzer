using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Strategy.Factory;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class ScoresCalculatorTest
    {
        [Test]
        public void Test()
        {
            TestFactory(StrategyFactoryType.Normal);
        }

        [Test]
        public void TestKd()
        {
            TestFactory(StrategyFactoryType.Kd);
        }

        [Test]
        public void TestRiseDown()
        {
            TestFactory(StrategyFactoryType.RiseDown);
        }

        [Test]
        public void TestSpike()
        {
            TestFactory(StrategyFactoryType.Spike);
        }

        [Test]
        public void TestVolume()
        {
            TestFactory(StrategyFactoryType.Volume);
        }

        void TestFactory(StrategyFactoryType tp)
        {
            ScoresCalculator calc = new ScoresCalculator();

            IStrategyFactory factory = StrategyFactoryCreater.Instance().CreateFactory(tp);

            FakeStockHistory fsh = new FakeStockHistory();
            fsh.Init();
            calc.Calc(fsh, factory, new FakeIBonusProcessor());
        }
    }
}
