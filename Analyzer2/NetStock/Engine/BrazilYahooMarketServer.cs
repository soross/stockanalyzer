using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class BrazilYahooMarketServer : AbstractYahooMarketServer
    {
        public BrazilYahooMarketServer(Country country) : base(country)
        {
        }

        protected override StockServer getStockServer(Country country)
        {
            return new BrazilYahooStockServer(country);
        }
    }
}
