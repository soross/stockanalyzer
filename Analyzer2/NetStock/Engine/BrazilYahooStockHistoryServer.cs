using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class BrazilYahooStockHistoryServer : AbstractYahooStockHistoryServer
    {
        public BrazilYahooStockHistoryServer(Country country, Code code)
            : base(country, code)
        {
        }

        public BrazilYahooStockHistoryServer(Country country, Code code, Duration duration)
            : base(country, code, duration)
        {
        }

        protected override StockServer getStockServer(Country country)
        {
            return new BrazilYahooStockServer(country);
        }
    }
}
