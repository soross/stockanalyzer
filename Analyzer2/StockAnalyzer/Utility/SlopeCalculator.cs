using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    // 股票一定周期内的趋势 （上升或者下降的斜率） 
    class SlopeCalculator : FixedContainer<double>
    {
        public SlopeCalculator(int totalCount)
            : base (totalCount)
        {
        }

        public override double GetValue()
        {
            double valLast = _Values.Last.Value;
            int totalCount = _Values.Count;

            List<double> arr = new List<double>(); 

            foreach (double val in _Values)
            {
                double delta = ((valLast - val) / val) / totalCount;
                arr.Add(delta);
                totalCount--;
            }

            double result = arr.Average() / XAxisMultiply;
            return result;
        }

        public bool IsRisePeriod()
        {
            double val = GetValue();
            return val > RISEDOWNMARGIN;
        }

        public bool IsDownPeriod()
        {
            double val = GetValue();
            return val < -RISEDOWNMARGIN;
        }

        const double RISEDOWNMARGIN = 8; // 判断上升或者下降的斜率 
        const double XAxisMultiply = 0.001; // X轴倍率 
    }
}
