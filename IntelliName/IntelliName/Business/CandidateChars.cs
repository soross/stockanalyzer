using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.Business
{
    class CandidateChars : ICandidateChars
    {
        public ICollection<char> GetGeneralChars()
        {
            return _GeneralChars.ToCharArray();
        }

        public void SetGeneralChars(string para)
        {
            _GeneralChars = para;
        }

        public ICollection<char> GetBoyChars()
        {
            return _BoyChars.ToCharArray();
        }

        public void SetBoyChars(string para)
        {
            _BoyChars = para;
        }

        public ICollection<char> GetGirlChars()
        {
            return _GirlChars.ToCharArray();
        }

        public void SetGirlChars(string para)
        {
            _GirlChars = para;
        }

        public ICollection<char> GetAvoidChars()
        {
            return _AvoidChars.ToCharArray();
        }

        public void SetAvoidChars(string para)
        {
            _AvoidChars = para;
        }

        string _GeneralChars;
        string _BoyChars;
        string _GirlChars;
        string _AvoidChars;
    }
}
