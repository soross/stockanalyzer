using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliName.Business;

namespace IntelliName.DB
{
    interface IDatabase
    {
        void AddLastNames(ICollection<string> lastnames);

        ICollection<string> LoadLastName();

        void AddPersonName(PersonName val);

        void Init();

        void Close();

        void SaveNameChars(NameChars val);

        ICollection<CharCount> LoadAllNameCharsByOrder();

        void SavePersonNames(ICollection<PersonName> arr);

        void LoadAllChars(ICandidateChars chars);
    }
}
