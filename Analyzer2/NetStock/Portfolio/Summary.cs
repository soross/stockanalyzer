using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Portfolio
{
    abstract class Summary<E>
    {
        public abstract bool add(E element);
        public abstract void add(int index, E element);
        public abstract E get(int index);
        public abstract E remove(int index);
        public abstract bool remove(E element);
        public abstract int size();
    }
}
