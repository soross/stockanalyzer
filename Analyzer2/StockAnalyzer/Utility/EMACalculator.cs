using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FinanceAnalyzer.Utility
{
    // Ref: http://en.wikipedia.org/wiki/Moving_average#Exponential_moving_average 
    public class EmaCalculator
    {
        private EmaCalculator()
        {
        }

        public static List<double> Calc(List<double> values)
        {
            if ((values == null) || values.Count == 0)
            {
                return null;
            }

            double alpha = 2.0 / (values.Count + 1);

            List<double> results = new List<double>();
            results.Add(values[0]);

            double ema = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                ema = ema * (1 - alpha) + values[i] * alpha;
                results.Add(ema);
            }

            return results;
        }
    }
}
