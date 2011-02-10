using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class SingaporeYahooStockServer : AbstractYahooStockServer
    {
        public SingaporeYahooStockServer(Country country)
            : base(country)
        {
        }

        protected override String getYahooCSVBasedURL()
        {
            return "http://sg.finance.yahoo.com/d/quotes.csv?s=";
        }
    }
}
