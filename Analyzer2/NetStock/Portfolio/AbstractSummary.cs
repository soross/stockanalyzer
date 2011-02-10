using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Portfolio
{
    class AbstractSummary<E> : Summary<E>
    {
        public override bool add(E element)
        {
            _list.Add(element);
            return true;
        }


        public override void add(int index, E element)
        {
            _list.Insert(index, element);
        }


        public override E get(int index)
        {
            if ((index < 0) || (index >= _list.Count))
            {
                return default(E);
            }

            return _list[index];
        }


        public override E remove(int index)
        {
            _list.RemoveAt(index); // Note: Not Remove() 
            return default(E);
        }


        public override bool remove(E element)
        {
            return _list.Remove(element);
        }


        public override int size()
        {
            return _list.Count;
        }

        // (Java) Take note on the protected access level. This is must have, in order for
        // child classes to behave correctly.
        protected Object readResolve()
        {
            throw new NotImplementedException();
        }

        private List<E> _list = new List<E>();
    }
}
