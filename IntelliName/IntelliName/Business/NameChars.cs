using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.Business
{
    class NameChars
    {
        public void Add(char namechar)
        {
            if (_CharToCounts.ContainsKey(namechar))
            {
                int count = _CharToCounts[namechar];
                count++;
                _CharToCounts[namechar] = count;
            }
            else
            {
                _CharToCounts.Add(namechar, 1);
            }
        }

        public void Add(char[] namechars)
        {
            foreach (char item in namechars)
            {
                Add(item);
            }
        }

        public ICollection<char> GetAllKeys()
        {
            return _CharToCounts.Keys;
        }

        public int GetCount(char val)
        {
            if (_CharToCounts.ContainsKey(val))
            {
                return _CharToCounts[val];
            }
            else
            {
                return 0;
            }
        }

        Dictionary<char, int> _CharToCounts = new Dictionary<char, int>();
    }
}
