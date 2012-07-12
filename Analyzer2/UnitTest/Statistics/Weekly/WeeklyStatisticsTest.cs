using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer.Statistics.Weekly
{
    [TestFixture]
    public class WeeklyStatisticsTest
    {
        [Test]
        public void Test()
        {
            // Check stocks in Database
            WeeklyStatistics stat = new WeeklyStatistics();

            FakeStockHistory hist = new FakeStockHistory();
            hist.Init();

            stat.Calc(hist);

            stat.PrintResult(new DummyLog());
        }
    }
}
