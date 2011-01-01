using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Strategy;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class ScoresCalculatorTest
    {
        [Test]
        public void Test()
        {
            ScoresCalculator calc = new ScoresCalculator();

            StrategyFactory factory = new StrategyFactory();
            factory.Init();

            calc.Calc(new FakeStockHistory(), factory, new FakeIBonusProcessor());
        }
    }
}
