using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class MovingAverageAlgorithm
    {
        public bool AddValue(double val)
        {
            return true;
        }

        MovingAveragePrediction prediction_ = new MovingAveragePrediction();
    }
}
