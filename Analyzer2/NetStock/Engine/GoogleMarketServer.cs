using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStock.Adapter.Jackson;

namespace DotNetStock.Engine
{
    class GoogleMarketServer : MarketServer
    {
        private Country country;
        private List<Index> indicies;
        private List<Code> codes = new List<Code>();
        private Dictionary<Code, Index> codeToIndexMap = new Dictionary<Code, Index>();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /**
         * Constructs a stock market index server based on country.
         *
         * @param country the country
         */
        public GoogleMarketServer(Country country)
        {
            this.country = country;
            this.indicies = Utils.getStockIndices(country);
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

        /**
         * Returns current stock market index information.
         *
         * @return current stock market index information
         */

        public Market getMarket()
        {
            try
            {
                return new GoogleMarket(this);
            }
            catch (StockNotFoundException exp)
            {
                log.Error(null, exp);
            }

            return null;
        }

        private class GoogleMarket : Market
        {
            private Dictionary<Index, Stock> map = new Dictionary<Index, Stock>();
            // Will it be better if we make this as static?
            private ObjectMapper mapper = new ObjectMapper();

            GoogleMarketServer _Svr;

            public GoogleMarket(GoogleMarketServer svr)
            {
                _Svr = svr;
                try
                {
                    // Use StringBuilder instead of StringBuffer. We do not concern
                    // on thread safety.
                    StringBuilder builder = new StringBuilder("http://www.google.com/finance/info?client=ig&q=");
                    // Exception will be thrown from apache httpclient, if we do not
                    // perform URL encoding.
                    builder.Append(_Svr.codes[0].toString());
                    int size = _Svr.codes.Count;

                    for (int i = 1; i < size; i++)
                    {
                        builder.Append(",");
                        builder.Append(_Svr.codes[i].toString());
                    }
                    String location = builder.ToString();
                    String respond = Gui.Utils.getResponseBodyAsStringBasedOnProxyAuthOption(location);
                    // Google returns "// [ { "id": ... } ]".
                    // We need to turn them into "[ { "id": ... } ]".
                    List<Dictionary<string, string>> jsonArray = mapper.readValue(Utils.GoogleRespondToJSON(respond), typeof(List<Dictionary<string, string>>));
                    List<Stock> stocks = new List<Stock>();
                    size = jsonArray.Count;
                    for (int i = 0; i < size; i++)
                    {
                        Dictionary<String, String> jsonObject = jsonArray[i];
                        double l_curr = Double.Parse(jsonObject["l_cur"].Replace(",", ""));
                        double c = Double.Parse(jsonObject["c"].Replace(",", ""));
                        // We ignore changePricePercentage. GoogleMarket doesn't
                        // need to return this value.
                        Stock stock = new Stock.Builder(_Svr.codes[i], Symbol.newInstance(_Svr.codes[i].toString())).lastPrice(l_curr).changePrice(c).build();
                        stocks.Add(stock);
                    }
                    // Store the result for later query purpose.
                    foreach (Stock stock in stocks)
                    {
                        map.Add(_Svr.codeToIndexMap[stock.getCode()], stock);
                    }
                }
                catch (Exception ex)
                {
                    // Jackson library may cause runtime exception if there is error
                    // in the JSON string.
                    throw new StockNotFoundException(null, ex);
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
                // Sad. Google doesn't give us volume information yet.
                return 0;
            }


            public double getValue()
            {
                return 0;
            }


            public Country getCountry()
            {
                return _Svr.country;
            }
        }
    }
}
