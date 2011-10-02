using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FinanceAnalyzer.Utility
{
    class ValueAverager<T> : FixedContainer<T>
    {
        public ValueAverager(int totalCount)
            : base(totalCount)
        {
        }

        public override double GetValue()
        {
            return _Values.Average(T => Convert.ToDouble(T, CultureInfo.CurrentCulture));
        }
    }
}
