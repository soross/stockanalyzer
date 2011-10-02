using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class NumbericHelper
    {
        private NumbericHelper()
        {
        }

        // Is same positive and negative 
        public static bool IsSameSign(double val1, double val2)
        {
            if ((val1 > 0) && (val2 > 0))
            {
                return true;
            }
            else if ((val1 < 0) && (val2 < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
