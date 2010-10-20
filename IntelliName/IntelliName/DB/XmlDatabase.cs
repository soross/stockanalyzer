using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliName.Business;
using System.Xml.Linq;
using System.Xml.XPath;
using IntelliName.DB.Xml;

namespace IntelliName.DB
{
    class XmlDatabase : IDatabase
    {
        public void AddLastNames(ICollection<string> lastnames)
        {
            XElement element = _Doc.Element("root");
            XElement lastnameElement = new XElement("LastName");
            
            foreach (string item in lastnames)
            {
                lastnameElement.AddFirst(new XElement("SurName", item));
            }

            element.AddFirst(lastnameElement);
        }

        public ICollection<string> LoadLastName()
        {
            List<string> arr = new List<string>();

            var elements = _Doc.XPathSelectElements("//SurName");
            foreach (var item in elements)
            {
                arr.Add(item.Value);
            }

            return arr;
        }

        public void AddPersonName(PersonName val)
        {
        }

        public void Init()
        {
            _Doc = XDocument.Load(ApplicationHelper.GetAppPath() + "\\chars.xml");
        }

        public void Close()
        {
            if (_Doc != null)
            {
                _Doc.Save(ApplicationHelper.GetAppPath() + "\\chars.xml");
            }            
        }

        public void SaveNameChars(NameChars val)
        {
            XElement element = _Doc.Element("root");
            XElement charElement = new XElement("CharCount");
            
            ICollection<char> arr = val.GetAllKeys();
            foreach (char ch in arr)
            {
                if (!InvalidChars.IsValidChar(ch))
                {
                    continue;
                }

                int count = val.GetCount(ch);
                charElement.Add(new XElement("Char", new XAttribute("Name", ch.ToString()), count));
            }

            element.Add(charElement);
        }

        public ICollection<CharCount> LoadAllNameCharsByOrder()
        {
            List<CharCount> arr = new List<CharCount>();

            var elements = from item in _Doc.XPathSelectElements("//Char") orderby item.Value descending select item;
            foreach (var item in elements)
            {
                string ch = item.Attribute("Name").Value;
                int count = int.Parse(item.Value);

                arr.Add(new CharCount() { CharVal = ch, Count = count });
            }

            return arr;
        }

        public void SavePersonNames(ICollection<PersonName> arr)
        {
            
        }

        public void LoadAllChars(ICandidateChars chars)
        {
            var item = _Doc.XPathSelectElement("//GeneralChars");
            chars.SetGeneralChars(item.Value);

            item = _Doc.XPathSelectElement("//BoyChars");
            chars.SetBoyChars(item.Value);

            item = _Doc.XPathSelectElement("//GirlChars");
            chars.SetGirlChars(item.Value);

            item = _Doc.XPathSelectElement("//AvoidChars");
            chars.SetAvoidChars(item.Value);
        }

        XDocument _Doc;
    }
}
