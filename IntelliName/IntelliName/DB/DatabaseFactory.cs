using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.DB
{
    class DatabaseFactory
    {
        public static IDatabase GetDB()
        {
            return _XmlDb;
        }

        static XmlDatabase _XmlDb = new XmlDatabase();
        static IDatabase _Db = new MongoDataBase();
    }
}
