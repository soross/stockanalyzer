using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class SingaporeYahooStockServerFactory : StockServerFactory
    {
        private SingaporeYahooStockServerFactory(Country country)
        {
            this.country = country;
            stockServer = new SingaporeYahooStockServer(country);
            marketServer = new SingaporeYahooMarketServer(country);
        }

        public static StockServerFactory newInstance(Country country)
        {
            return new SingaporeYahooStockServerFactory(country);
        }

        public StockServer getStockServer()
        {
            return stockServer;
        }

        public StockHistoryServer getStockHistoryServer(Code code)
        {
            try
            {
                return new SingaporeYahooStockHistoryServer(country, code);
            }
            catch (StockHistoryNotFoundException exp)
            {
                log.Error(null, exp);
                return null;
            }
        }

        public StockHistoryServer getStockHistoryServer(Code code, Duration duration)
        {
            try
            {
                return new SingaporeYahooStockHistoryServer(country, code, duration);
            }
            catch (StockHistoryNotFoundException exp)
            {
                log.Error(null, exp);
                return null;
            }
        }

        public MarketServer getMarketServer()
        {
            return marketServer;
        }

        private StockServer stockServer;
        private MarketServer marketServer;
        private Country country;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
