using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class SimpleAverager<T>
    {
        public void AddVal(T val)
        {
            _Values.AddLast(val);
        }

        public double GetAverage()
        {
            return _Values.Average(T => Convert.ToDouble(T));
        }

        protected LinkedList<T> _Values = new LinkedList<T>();
    }
}
