using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Linq;
using IntelliName.Business;
using System.Configuration;
using System.Threading;

namespace IntelliName.DB
{
    class MongoDataBase : IDatabase
    {
        public void Init()
        {
            try
            {
                var connstr = ConfigurationManager.AppSettings["mongodbconnection"];
                if (String.IsNullOrEmpty(connstr))
                    throw new ArgumentNullException("Connection string not found.");

                _instance = new Mongo(connstr);
                _instance.Connect();

                _db = _instance.GetDatabase("IntelliName");
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
            }
        }

        public void Close()
        {
            _instance.Disconnect();
        }

        public void AddLastNames(ICollection<string> lastnames)
        {
            var names = _db.GetCollection("LastNames");

            foreach (string item in lastnames)
            {
                var doc = new Document();
                doc["LastName"] = item;

                names.Insert(doc);
            }
        }

        public ICollection<string> LoadLastName()
        {
            List<string> arr = new List<string>();

            var names = _db.GetCollection("LastNames");

            foreach (var item in names.FindAll().Documents)
            {
                string val = item["LastName"] as string;
                arr.Add(val);
            }

            _log.Info("LoadLastName: Last Name Count = " + names.Count());
            return arr;
        }

        public void AddPersonName(PersonName val)
        {
            try
            {
                var names = _db.GetCollection("PersonNames");

                var item = new Document();
                item["LastName"] = val.LastName;
                item["FirstName"] = val.FirstName;

                names.Insert(item);
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
            }
        }

        public void SaveNameChars(NameChars val)
        {
            try
            {
                var names = _db.GetCollection<CharCount>();

                ICollection<char> arr = val.GetAllKeys();
                foreach (char ch in arr)
                {
                    int count = val.GetCount(ch);

                    names.Insert(new CharCount() { CharVal = ch.ToString(), Count = count });
                }

                _log.Info("SaveNameChars: Char Count = " + names.Count());
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
            }
        }

        public ICollection<CharCount> LoadAllNameCharsByOrder()
        {
            try
            {
                var names = _db.GetCollection<CharCount>();

                var result = from item in _db.GetCollection<CharCount>().Linq() orderby item.Count descending select item;

                foreach (var item in result)
                {
                    _log.Info("Char: " + item.CharVal + ", Count: " + item.Count);
                }

                // TODO
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
                return null;
            }
        }

        public void SavePersonNames(ICollection<PersonName> arr)
        {
            try
            {
                var names = _db.GetCollection<PersonName>();

                names.Insert<PersonName>(arr);

                _log.Info("SavePersonNames: PersonName Count = " + names.Count());
            }
            catch (Exception ex)
            {
                _log.Error(ex.StackTrace);
            }
        }

        Mongo _instance;
        IMongoDatabase _db;
        // Create a logger for use in this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
