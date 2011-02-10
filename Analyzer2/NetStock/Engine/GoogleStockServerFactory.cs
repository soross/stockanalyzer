using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class GoogleStockServerFactory : StockServerFactory
    {
        private GoogleStockServerFactory(Country country)
        {
            marketServer = new GoogleMarketServer(country);
        }

        /**
         * Returns GoogleStockServerFactory based on given country.
         *
         * @param country the country
         * @return GoogleStockServerFactory based on given country
         */
        public static StockServerFactory newInstance(Country country)
        {
            return new GoogleStockServerFactory(country);
        }

        /**
         * Returns stock server for this factory.
         *
         * @return stock server for this factory
         */

        public StockServer getStockServer()
        {
            return Utils.emptyStockServer();
        }

        /**
         * Returns stock history server for this factory based on given code. <code>
         * null</code> will be returned if fail.
         *
         * @param code the code
         * @return stock history server for this factory based on given code. <code>
         * null</code> will be returned if fail
         */

        public StockHistoryServer getStockHistoryServer(Code code)
        {
            return null;
        }

        /**
         * Returns stock history server for this factory based on given code and 
         * duration. <code>null</code> will be returned if fail.
         * 
         * @param code the code
         * @param duration the duration
         * @return stock history server for this factory based on given code and 
         * duration. <code>null</code> will be returned if fail
         */

        public StockHistoryServer getStockHistoryServer(Code code, Duration duration)
        {
            return null;
        }

        /**
         * Returns market server for this factory.
         *
         * @return market server for this factory
         */

        public MarketServer getMarketServer()
        {
            return marketServer;
        }

        private MarketServer marketServer;
    }
}
