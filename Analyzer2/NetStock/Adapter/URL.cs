using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Adapter
{
    class URL
    {
        public URL()
        {
        }

        public URL(string str)
        {
            _UrlString = str;
        }

        public URL(URL baseUrl, string str)
        {
            _UrlString = str;
        }

        public string toString()
        {
            return _UrlString;
        }

        string _UrlString;
    }
}
