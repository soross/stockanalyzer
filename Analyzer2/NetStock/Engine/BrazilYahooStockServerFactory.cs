using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class BrazilYahooStockServerFactory : StockServerFactory
    {
        private BrazilYahooStockServerFactory(Country country)
        {
            this.country = country;
            stockServer = new BrazilYahooStockServer(country);
            marketServer = new BrazilYahooMarketServer(country);
        }

        public static StockServerFactory newInstance(Country country)
        {
            return new BrazilYahooStockServerFactory(country);
        }

        public StockServer getStockServer()
        {
            return stockServer;
        }

        public StockHistoryServer getStockHistoryServer(Code code)
        {
            try
            {
                return new BrazilYahooStockHistoryServer(country, code);
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
                return new BrazilYahooStockHistoryServer(country, code, duration);
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
