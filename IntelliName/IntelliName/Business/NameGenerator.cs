using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliName.DB;
using Microsoft.International.Converters.PinYinConverter;
using System.Threading;
using IntelliName.Log;

namespace IntelliName.Business
{
    enum NameType
    {
        Boy,
        Girl
    }

    class NameGenerator
    {
        public NameGenerator()
        {
            ICollection<CharCount> arr = DatabaseFactory.GetDB().LoadAllNameCharsByOrder();

            _AllChars = from item in arr select item.CharVal;

            DatabaseFactory.GetDB().LoadAllChars(_Chars);

            InitPinyins();
        }

        public ICollection<string> Generate(int count)
        {
            List<string> arr = new List<string>();

            for (int i = 0; i < count; i++)
            {
                arr.Add(GenerateOne(NameType.Girl));
            }

            return arr;
        }

        private string GenerateOne(NameType para)
        {
            List<char> arr = new List<char>();
            arr.AddRange(_Chars.GetGeneralChars());

            if (para == NameType.Boy)
            {
                arr.AddRange(_Chars.GetBoyChars());
            }
            else if (para == NameType.Girl)
            {
                arr.AddRange(_Chars.GetGirlChars());
            }

            char c1 = RandomChar(arr);

            while (IsInvalidChar(c1))
            {
                c1 = RandomChar(arr);
            }

            char c2 = RandomChar(arr);
            while (IsInvalidChar(c2))
            {
                c2 = RandomChar(arr);
            }

            return c1.ToString() + c2.ToString();
        }

        private string GenerateOne2()
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
            Thread.Sleep(126);

            Random rnd = new Random((int)DateTime.Now.Ticks);

            Thread.Sleep(rnd.Next(100));

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
            if (INVALIDCHARS.Contains(para))
            {
                return true;
            }

            try
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
            catch (NotSupportedException ex)
            {
                LogFactory.Instance().Log.Log(ex.StackTrace);
                return true;
            }
        }

        IEnumerable<string> _AllChars;

        const string INVALIDCHARS = " \t\n";

        ICandidateChars _Chars = new CandidateChars();

        List<string> _AllPinyins = new List<string>();
    }
}
