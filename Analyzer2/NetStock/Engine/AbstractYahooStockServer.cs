using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    abstract class AbstractYahooStockServer : StockServer
    {
        protected abstract String getYahooCSVBasedURL();

        public AbstractYahooStockServer(Country country)
        {
            this.country = country;
        }

        public Country getCountry()
        {
            return this.country;
        }

        private bool isToleranceAllowed(int currSize, int expectedSize)
        {
            if (currSize >= expectedSize)
            {
                return true;
            }
            if (expectedSize <= 0)
            {
                return true;
            }
            double result = 100.0 - ((double)(expectedSize - currSize) / (double)expectedSize * 100.0);
            return (result >= STABILITY_RATE);
        }

        public List<Stock> getStocksByCodes(List<Code> codes)
        {
            List<Symbol> symbols = new List<Symbol>();
            foreach (Code code in codes)
            {
                Code newCode = Utils.toYahooFormat(code, this.country);
                symbols.Add(Symbol.newInstance(newCode.toString()));
            }
            return getStocksBySymbols(symbols);
        }

        public Stock getStock(Code code)
        {
            Code newCode = Utils.toYahooFormat(code, this.country);
            return getStock(Symbol.newInstance(newCode.toString()));
        }

        public List<Stock> getStocksBySymbols(List<Symbol> symbols)
        {
            List<Stock> stocks = new List<Stock>();

            if (symbols.Count == 0)
            {
                return stocks;
            }

            int time = symbols.Count / MAX_STOCK_PER_ITERATION;
            int remainder = symbols.Count % MAX_STOCK_PER_ITERATION;

            for (int i = 0; i < time; i++)
            {
                int start = i * MAX_STOCK_PER_ITERATION;
                int end = start + MAX_STOCK_PER_ITERATION;

                StringBuilder stringBuilder = new StringBuilder(getYahooCSVBasedURL());
                StringBuilder symbolBuilder = new StringBuilder();
                List<Symbol> expectedSymbols = new List<Symbol>();

                int endLoop = end - 1;
                for (int j = start; j < endLoop; j++)
                {
                    String symbolString1 = null;

                    try
                    {
                        //symbolString = java.net.URLEncoder.encode(symbols.get(j).toString(), "UTF-8");
                        symbolString1 = symbols[j].toString();
                    }
                    catch (Exception ex)
                    {
                        throw new StockNotFoundException();
                    }
                    symbolBuilder.Append(symbolString1).Append("+");
                    expectedSymbols.Add(symbols[j]);
                }

                String symbolString = null;

                try
                {
                    //symbolString = java.net.URLEncoder.encode(symbols.get(end - 1).toString(), "UTF-8");
                    symbolString = symbols[end - 1].toString();
                }
                catch (Exception ex)
                {
                    throw new StockNotFoundException();
                }

                symbolBuilder.Append(symbolString);
                expectedSymbols.Add(symbols[end - 1]);

                String _symbol = symbolBuilder.ToString();

                stringBuilder.Append(_symbol).Append(YAHOO_STOCK_FORMAT);

                String location = stringBuilder.ToString();

                bool success = false;

                for (int retry = 0; retry < NUM_OF_RETRY; retry++)
                {
                    String respond = Gui.Utils.getResponseBodyAsStringBasedOnProxyAuthOption(location);

                    if (respond == null)
                    {
                        continue;
                    }

                    List<Stock> tmpStocks = YahooStockFormat.getInstance().parse(respond);
                    if (tmpStocks.Count != MAX_STOCK_PER_ITERATION)
                    {
                        if (retry == (NUM_OF_RETRY - 1))
                        {

                            int currSize = tmpStocks.Count;
                            int expectedSize = expectedSymbols.Count;

                            if (this.isToleranceAllowed(currSize, expectedSize))
                            {
                                List<Symbol> currSymbols = new List<Symbol>();
                                List<Stock> emptyStocks = new List<Stock>();

                                foreach (Stock stock in tmpStocks)
                                {
                                    currSymbols.Add(stock.getSymbol());
                                }

                                foreach (Symbol symbol in expectedSymbols)
                                {
                                    if (currSymbols.Contains(symbol) == false)
                                    {
                                        emptyStocks.Add(Gui.Utils.getEmptyStock(Code.newInstance(symbol.toString()), symbol));
                                    }
                                }

                                tmpStocks.AddRange(emptyStocks);
                            }
                            else
                            {
                                throw new StockNotFoundException("Expect " + expectedSize + " stock(s), but only receive " + currSize + " stock(s) from " + location);
                            }
                        }   // if (retry == (NUM_OF_RETRY-1))
                        continue;
                    }   // if (tmpStocks.Count != MAX_STOCK_PER_ITERATION)

                    stocks.AddRange(tmpStocks);

                    success = true;
                    break;
                }

                if (success == false)
                {
                    throw new StockNotFoundException("Stock size (" + stocks.Count + ") inconsistent with symbol size (" + symbols.Count + ")");
                }
            }

            int start2 = symbols.Count - remainder;
            int end2 = start2 + remainder;

            StringBuilder stringBuilder2 = new StringBuilder(getYahooCSVBasedURL());
            StringBuilder symbolBuilder2 = new StringBuilder();
            List<Symbol> expectedSymbols2 = new List<Symbol>();

            int endLoop2 = end2 - 1;
            for (int i = start2; i < endLoop2; i++)
            {
                String symbolString = symbols[i].toString();

                symbolBuilder2.Append(symbolString).Append("+");
                expectedSymbols2.Add(symbols[i]);
            }

            String symbolString2 = symbols[end2 - 1].toString();

            symbolBuilder2.Append(symbolString2);
            expectedSymbols2.Add(symbols[end2 - 1]);

            String _symbol2 = symbolBuilder2.ToString();

            stringBuilder2.Append(_symbol2).Append(YAHOO_STOCK_FORMAT);

            String location2 = stringBuilder2.ToString();

            for (int retry = 0; retry < NUM_OF_RETRY; retry++)
            {
                String respond = Gui.Utils.getResponseBodyAsStringBasedOnProxyAuthOption(location2);
                if (respond == null)
                {
                    continue;
                }
                List<Stock> tmpStocks = YahooStockFormat.getInstance().parse(respond);
                if (tmpStocks.Count != remainder)
                {
                    if (retry == (NUM_OF_RETRY - 1))
                    {
                        int currSize = tmpStocks.Count;
                        int expectedSize = expectedSymbols2.Count;

                        if (this.isToleranceAllowed(currSize, expectedSize))
                        {
                            List<Symbol> currSymbols = new List<Symbol>();
                            List<Stock> emptyStocks = new List<Stock>();

                            foreach (Stock stock in tmpStocks)
                            {
                                currSymbols.Add(stock.getSymbol());
                            }

                            foreach (Symbol symbol in expectedSymbols2)
                            {
                                if (currSymbols.Contains(symbol) == false)
                                {
                                    emptyStocks.Add(Gui.Utils.getEmptyStock(Code.newInstance(symbol.toString()), symbol));
                                }
                            }

                            tmpStocks.AddRange(emptyStocks);
                        }
                        else
                        {
                            throw new StockNotFoundException("Expect " + expectedSize 
                                + " stock(s), but only receive " + currSize + " stock(s) from " + location2);
                        }
                    }   // if (retry == (NUM_OF_RETRY-1))

                    continue;
                }   // if (tmpStocks.Count != remainder)

                stocks.AddRange(tmpStocks);

                break;
            }

            if (stocks.Count != symbols.Count)
            {
                throw new StockNotFoundException("Stock size (" + stocks.Count + ") inconsistent with symbol size (" + symbols.Count + ")");
            }

            return stocks;
        }


        public Stock getStock(Symbol symbol)
        {
            StringBuilder stringBuilder = new StringBuilder(getYahooCSVBasedURL());

            String _symbol = symbol.toString();

            stringBuilder.Append(_symbol).Append(YAHOO_STOCK_FORMAT);

            String location = stringBuilder.ToString();

            for (int retry = 0; retry < NUM_OF_RETRY; retry++)
            {
                String respond = Gui.Utils.getResponseBodyAsStringBasedOnProxyAuthOption(location);
                if (respond == null)
                {
                    continue;
                }
                List<Stock> stocks = YahooStockFormat.getInstance().parse(respond);

                if (stocks.Count == 1)
                {
                    return stocks[0];
                }

                break;
            }

            throw new StockNotFoundException(symbol.toString());
        }

        public virtual List<Stock> getAllStocks()
        {
            // Java project doesn't check this. 
            throw new NotImplementedException();
        }

        // Yahoo server limit is 200. We shorter, to avoid URL from being too long.
        // Yahoo sometimes does complain URL for being too long.
        private const int MAX_STOCK_PER_ITERATION = 180;
        private const int NUM_OF_RETRY = 2;

        // Yahoo server's result is not stable. If we request for 100 stocks, it may only
        // return 99 stocks to us. We allow stability rate in %. Higher rate means more
        // stable.
        private const double STABILITY_RATE = 90.0;

        // Update on 19 March 2009 : We cannot assume certain parameters will always
        // be float. They may become integer too. For example, in the case of Korea
        // Stock Market, Previous Close is in integer. We shall apply string quote
        // protection method too on them.
        //
        // Here are the index since 19 March 2009 :
        // (0) Symbol
        // (1) Name
        // (2) Stock Exchange
        // (3) Symbol
        // (4) Previous Close
        // (5) Symbol
        // (6) Open
        // (7) Symbol
        // (8) Last Trade
        // (9) Symbol
        // (10) Day's high
        // (11) Symbol
        // (12) Day's low
        // (13) Symbol
        // (14) Volume
        // (15) Symbol
        // (16) Change
        // (17) Symbol
        // (18) Change Percent
        // (19) Symbol
        // (20) Last Trade Size
        // (21) Symbol
        // (22) Bid
        // (23) Symbol
        // (24) Bid Size
        // (25) Symbol
        // (26) Ask
        // (27) Symbol
        // (28) Ask Size
        // (29) Symbol
        // (30) Last Trade Date
        // (31) Last Trade Time.
        //
        // s = Symbol
        // n = Name
        // x = Stock Exchange
        // o = Open             <-- Although we will keep this value in our stock data structure, we will not show
        //                          it to clients. As some stock servers unable to retrieve open price.
        // p = Previous Close
        // l1 = Last Trade (Price Only)
        // h = Day's high
        // g = Day's low
        // v = Volume           <-- We need to take special care on this, it may give us 1,234. This will
        //                          make us difficult to parse csv file. The only workaround is to make integer
        //                          in between two string literal (which will always contains "). By using regular
        //                          expression, we will manually remove the comma.
        // c1 = Change
        // p2 = Change Percent
        // k3 = Last Trade Size <-- We need to take special care on this, it may give us 1,234...
        // b3 = Bid (Real-time) <-- We use b = Bid previously. However, most stocks return 0.
        // b6 = Bid Size        <-- We need to take special care on this, it may give us 1,234...
        // b2 = Ask (Real-time) <-- We use a = Ask previously. However, most stocks return 0.
        // a5 = Ask Size        <-- We need to take special care on this, it may give us 1,234...
        // d1 = Last Trade Date
        // t1 = Last Trade Time
        //
        // c6k2c1p2c -> Change (Real-time), Change Percent (Real-time), Change, Change in Percent, Change & Percent Change
        // "+1400.00","N/A - +4.31%",+1400.00,"+4.31%","+1400.00 - +4.31%"
        //
        // "MAERSKB.CO","AP MOELLER-MAERS-","Copenhagen",32500.00,33700.00,34200.00,33400.00,660,"+1200.00","N/A - +3.69%",33,33500.00,54,33700.00,96,"11/10/2008","10:53am"
        private static String YAHOO_STOCK_FORMAT = "&f=snxspsosl1shsgsvsc1sp2sk3sb3sb6sb2sa5sd1t1";

        private Country country;
    }
}
