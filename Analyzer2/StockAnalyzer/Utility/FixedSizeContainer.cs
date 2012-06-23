using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class FixedSizeContainer<T>
    {
        public FixedSizeContainer(int totalCount)
        {
            FixedSize_ = totalCount;
        }

        public bool IsEnough()
        {
            return list_.Count == FixedSize_;
        }

        public void Add(T val)
        {
            if (list_.Count >= FixedSize_)
            {
                list_.RemoveAt(0);
            }

            list_.Add(val);
        }

        public T GetAt(int pos)
        {
            return list_[pos];
        }

        List<T> list_ = new List<T>();
        int FixedSize_;
    }
}
