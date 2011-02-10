using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class SingaporeYahooMarketServer : AbstractYahooMarketServer
    {
        public SingaporeYahooMarketServer(Country country)
            : base(country)
        {
        }

        protected override StockServer getStockServer(Country country)
        {
            return new SingaporeYahooStockServer(country);
        }
    }
}
