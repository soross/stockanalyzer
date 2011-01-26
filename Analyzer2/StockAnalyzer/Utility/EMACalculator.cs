using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FinanceAnalyzer.Utility
{
    // Ref: http://en.wikipedia.org/wiki/Moving_average#Exponential_moving_average 
    public class EmaCalculator
    {
        public static List<double> Calc(List<double> arr)
        {
            double alpha = 2.0 / (arr.Count + 1);

            List<double> results = new List<double>();
            results.Add(arr[0]);

            double ema = arr[0];

            for (int i = 1; i < arr.Count; i++)
            {
                ema = ema * (1 - alpha) + arr[i] * alpha;
                results.Add(ema);
            }

            return results;
        }
    }
}
