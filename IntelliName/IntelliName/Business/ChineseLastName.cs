using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliName.DB;

namespace IntelliName.Business
{
    // 中国的姓
    class ChineseLastName
    {
        public bool Find(string lastname)
        {
            return _LastNames.Contains(lastname);
        }

        public void Add(string lastname)
        {
            _LastNames.Add(lastname);
        }

        public void SaveToDb()
        {
            DatabaseFactory.GetDB().AddLastNames(_LastNames);
        }

        public void Load()
        {
            ICollection<string> arr = DatabaseFactory.GetDB().LoadLastName();

            foreach (string item in arr)
            {
                _LastNames.Add(item);
            }
        }

        HashSet<string> _LastNames = new HashSet<string>();
    }
}
