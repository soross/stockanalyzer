using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IntelliName.DB;

namespace IntelliName.Business
{
    class PersonNames
    {
        public void Init()
        {
            _AllLastNames.Load();
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Parse(s);
                }
            }

            DatabaseFactory.GetDB().SaveNameChars(_NameChars);

            //DatabaseFactory.GetDB().SavePersonNames(_Names);
        }

        private void Parse(string s)
        {
            //_log.Info("Current String: " + s);

            string[] arr = s.Split(new char[] { ' ', '	' });

            foreach (string val in arr)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    AddPersonName(val);
                }
            }
        }

        public void AddPersonName(string name)
        {
            name = name.Trim();

            if ((name.Length > 3) || (name.Length < 2))
            {
                return;
            }

            string lastName = name.Substring(0, 1);
            string firstName = name.Substring(1); // 名

            if (!_AllLastNames.Find(lastName))
            {
                return;
            }

            _Names.Add(new PersonName() { FirstName = firstName, LastName = lastName });

            _NameChars.Add(firstName.ToCharArray());

            //_log.Info("Name: " + name);
        }
        
        NameChars _NameChars = new NameChars();

        ChineseLastName _AllLastNames = new ChineseLastName(); // 中国的姓

        List<PersonName> _Names = new List<PersonName>();

        // Create a logger for use in this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
