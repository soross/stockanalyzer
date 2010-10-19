using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliName.DB;

namespace IntelliName.Business
{
    class NameGenerator
    {
        public NameGenerator()
        {
            ICollection<CharCount> arr = DatabaseFactory.GetDB().LoadAllNameCharsByOrder();

            _AllChars = from item in arr select item.CharVal;
        }

        public ICollection<string> Generate(int count)
        {
            return null;
        }

        IEnumerable<string> _AllChars;
    }
}
