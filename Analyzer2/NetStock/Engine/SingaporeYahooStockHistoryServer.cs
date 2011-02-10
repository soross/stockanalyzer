using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class SingaporeYahooStockHistoryServer : AbstractYahooStockHistoryServer
    {
        public SingaporeYahooStockHistoryServer(Country country, Code code)
            : base(country, code)
        {
        }

        public SingaporeYahooStockHistoryServer(Country country, Code code, Duration duration)
            : base(country, code, duration)
        {
        }

        protected override StockServer getStockServer(Country country)
        {
            return new SingaporeYahooStockServer(country);
        }
    }
}
