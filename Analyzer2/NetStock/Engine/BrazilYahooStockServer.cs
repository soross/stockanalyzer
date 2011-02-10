using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class BrazilYahooStockServer : AbstractYahooStockServer
    {
        public BrazilYahooStockServer(Country country) : base(country)
        {
        }

        protected override String getYahooCSVBasedURL()
        {
            return "http://br.finance.yahoo.com/d/quotes.csv?s=";
        }
    }
}
