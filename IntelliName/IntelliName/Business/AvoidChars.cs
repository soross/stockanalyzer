using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.International.Converters.PinYinConverter;

namespace IntelliName.Business
{
    class AvoidChars
    {
        public bool IsAvoidChars(char c1)
        {
            return AllAvoidChars.Contains(c1);
        }

        public bool IsSamePinyin(char c1)
        {
            ChineseChar chineseChar = new ChineseChar(c1);

            foreach (string item in chineseChar.Pinyins)
            {
                if (_AllPinyins.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        // 根据字符初始化所有的拼音 
        public void InitPinyins()
        {
            foreach (char c1 in AllAvoidChars)
            {
                ChineseChar chineseChar = new ChineseChar(c1);

                _AllPinyins.AddRange(chineseChar.Pinyins);
            }
        }

        public void Init(string line)
        {
            AllAvoidChars = line.ToCharArray();
        }

        public char[] AllAvoidChars
        {
            get;
            set;
        }

        private List<string> _AllPinyins = new List<string>();
    }
}
