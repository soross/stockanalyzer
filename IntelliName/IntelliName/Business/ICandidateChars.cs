using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.Business
{
    interface ICandidateChars
    {
        ICollection<char> GetGeneralChars();
        void SetGeneralChars(string para);

        ICollection<char> GetBoyChars();
        void SetBoyChars(string para);

        ICollection<char> GetGirlChars();
        void SetGirlChars(string para);

        ICollection<char> GetAvoidChars();
        void SetAvoidChars(string para);
    }
}
