using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class YahooMarketServer : AbstractYahooMarketServer
    {
        public YahooMarketServer(Country country)
            : base(country)
        {
        }

        protected override StockServer getStockServer(Country country)
        {
            return new YahooStockServer(country);
        }
    }
}
