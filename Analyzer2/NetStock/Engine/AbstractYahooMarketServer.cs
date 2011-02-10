using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    abstract class AbstractYahooMarketServer : MarketServer
    {
        private StockServer stockServer;
        private Country country;
        private List<Index> indicies;
        private List<Code> codes = new List<Code>();
        private Dictionary<Code, Index> codeToIndexMap = new Dictionary<Code, Index>();

        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected abstract StockServer getStockServer(Country country);

        public AbstractYahooMarketServer(Country country)
        {
            this.country = country;
            this.stockServer = getStockServer(country);
            /* Hack on Malaysia Market! The format among Yahoo and CIMB are difference. */
            if (country == Country.Malaysia)
            {
                List<Index> tmp = new List<Index>();
                foreach (Index index in Utils.getStockIndices(country))
                {
                    if (IndexHelper.Instance().GetIndexCode(index).toString().StartsWith("^"))
                    {
                        tmp.Add(index);
                    }
                }
                this.indicies = tmp;
            }
            else
            {
                this.indicies = Utils.getStockIndices(country);
            }
            if (this.indicies.Count == 0)
            {
                throw new ArgumentException(country.ToString());
            }
            foreach (Index index in indicies)
            {
                Code curCode = IndexHelper.Instance().GetIndexCode(index);
                codes.Add(curCode);
                codeToIndexMap.Add(curCode, index);
            }
        }

        public Market getMarket()
        {
            try
            {
                return new YahooMarket(this);
            }
            catch (StockNotFoundException exp)
            {
                log.Error("", exp);
            }

            return null;
        }

        private class YahooMarket : Market
        {
            private Dictionary<Index, Stock> map = new Dictionary<Index, Stock>();

            AbstractYahooMarketServer _Server;

            public YahooMarket(AbstractYahooMarketServer svr)
            {
                _Server = svr;

                List<Stock> stocks;

                try
                {
                    stocks = svr.stockServer.getStocksByCodes(svr.codes);
                }
                catch (StockNotFoundException ex)
                {
                    throw ex;
                }

                foreach (Stock stock in stocks)
                {
                    map.Add(svr.codeToIndexMap[stock.getCode()], stock);
                }
            }

            public double getIndex(Index index)
            {
                Stock stock = map[index];
                if (stock == null)
                {
                    return 0.0;
                }
                return stock.getLastPrice();
            }

            public double getChange(Index index)
            {
                Stock stock = map[index];
                if (stock == null)
                {
                    return 0.0;
                }
                return stock.getChangePrice();
            }

            public int getNumOfStockChange(ChangeType type)
            {
                return 0;
            }

            public long getVolume()
            {
                long total = 0;
                foreach (Stock stock in map.Values)
                {
                    total += stock.getVolume();
                }
                return total;
            }

            public double getValue()
            {
                double total = 0;
                foreach (Stock stock in map.Values)
                {
                    total += stock.getLastPrice();
                }
                return total;
            }

            public Country getCountry()
            {
                return _Server.country;
            }
        }
    }
}