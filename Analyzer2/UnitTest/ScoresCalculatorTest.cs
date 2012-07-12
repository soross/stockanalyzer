using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Stock;

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

            FakeStockHistory fsh = new FakeStockHistory();
            fsh.Init();
            calc.Calc(fsh, factory, new FakeIBonusProcessor());
        }
    }
}
