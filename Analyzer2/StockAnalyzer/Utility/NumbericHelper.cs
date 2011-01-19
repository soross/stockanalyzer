using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class NumbericHelper
    {
        // Is same positive and negative 
        public static bool IsSameSign(double val1, double val2)
        {
            return (val1 * val2) > 0;
        }
    }
}
