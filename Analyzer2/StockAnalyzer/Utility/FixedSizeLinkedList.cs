using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class FixedSizeLinkedList<T>
    {
        public FixedSizeLinkedList(int totalCount)
        {
            FixedSize_ = totalCount;
        }

        public bool IsEnough()
        {
            return list_.Count == FixedSize_;
        }

        public void AddLast(T val)
        {
            if (list_.Count >= FixedSize_)
            {
                list_.RemoveFirst();
            }

            list_.AddLast(val);
        }

        public T GetAt(int pos)
        {
            return list_.ElementAt(pos);
        }

        public T FindMax()
        {
            return list_.Max();
        }

        public T FindMin()
        {
            return list_.Min();
        }

        LinkedList<T> list_ = new LinkedList<T>();

        int FixedSize_;
    }
}
