using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    abstract class FixedContainer<T>
    {
        public FixedContainer(int totalCount)
        {
            _Count = totalCount;
        }

        public bool IsEnough()
        {
            return _Values.Count == _Count;
        }

        public void AddVal(T val)
        {
            if (_Values.Count >= _Count)
            {
                _Values.RemoveFirst();
            }

            _Values.AddLast(val);
        }

        abstract public double GetValue();

        protected int _Count;

        protected LinkedList<T> _Values = new LinkedList<T>();
    }
}
