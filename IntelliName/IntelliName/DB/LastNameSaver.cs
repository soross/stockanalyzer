using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IntelliName.Business;

namespace IntelliName.DB
{
    class LastNameSaver
    {
        public void Save(string fileName)
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

            _AllLastNames.SaveToDb();
        }

        private void Parse(string s)
        {
            string[] arr = s.Split(new char[] { ' ', '	' });

            foreach (string val in arr)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    _AllLastNames.Add(val);                    
                }                
            }
        }

        ChineseLastName _AllLastNames = new ChineseLastName();
    }
}
