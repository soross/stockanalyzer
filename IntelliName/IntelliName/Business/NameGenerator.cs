using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliName.DB;
using Microsoft.International.Converters.PinYinConverter;

namespace IntelliName.Business
{
    class NameGenerator
    {
        public NameGenerator()
        {
            ICollection<CharCount> arr = DatabaseFactory.GetDB().LoadAllNameCharsByOrder();

            _AllChars = from item in arr select item.CharVal;

            DatabaseFactory.GetDB().LoadAllChars(_Chars);
        }

        public ICollection<string> Generate(int count)
        {
            List<string> arr = new List<string>();

            for (int i = 0; i < count; i++ )
            {
                arr.Add(GenerateOne());
            }

            return arr;
        }

        private string GenerateOne()
        {
            char c1 = RandomChar(_Chars.GetGeneralChars());

            while (IsInvalidChar(c1))
            {
                c1 = RandomChar(_Chars.GetGeneralChars());
            }

            char c2 = RandomChar(_Chars.GetGeneralChars());
            while (IsInvalidChar(c2))
            {
                c2 = RandomChar(_Chars.GetGeneralChars());
            }

            return c1.ToString() + c2.ToString();
        }

        char RandomChar(ICollection<char> arr)
        {
            Random rnd = new Random();

            int pos = rnd.Next(arr.Count);
            char ch = arr.ElementAt(pos);

            return ch;
        }

        private void InitPinyins()
        {
            foreach (char item in _Chars.GetAvoidChars())
            {
                ChineseChar chChar = new ChineseChar(item);

                _AllPinyins.AddRange(chChar.Pinyins);
            }
        }

        private bool IsInvalidChar(char para)
        {
            ChineseChar chChar = new ChineseChar(para);

            foreach (string pron in chChar.Pinyins)
            {
                if (_AllPinyins.Contains(pron))
                {
                    return true;
                }
            }
            return false;
        }

        IEnumerable<string> _AllChars;

        ICandidateChars _Chars = new CandidateChars();

        List<string> _AllPinyins = new List<string>();
    }
}
