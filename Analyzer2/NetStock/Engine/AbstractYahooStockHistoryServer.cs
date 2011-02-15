using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DotNetStock.Engine
{
    abstract class AbstractYahooStockHistoryServer : StockHistoryServer
    {
        protected abstract StockServer getStockServer(Country country);

        public AbstractYahooStockHistoryServer(Country country, Code code)
            : this(country, code, DEFAULT_HISTORY_DURATION)
        {

        }

        public AbstractYahooStockHistoryServer(Country country, Code code, Duration duration)
        {
            if (code == null || duration == null)
            {
                throw new ArgumentException("Code or duration cannot be null");
            }

            this.country = country;
            this.code = Utils.toYahooFormat(code, country);
            this.duration = duration;
            try
            {
                buildHistory(this.code);
            }
            catch (OutOfMemoryException exp)
            {
                // Thrown from method.getResponseBodyAsString
                log.Error(null, exp);
                throw new StockHistoryNotFoundException("Out of memory");
            }
        }

        private bool parse(String respond)
        {
            historyDatabase.Clear();
            simpleDates.Clear();

            SimpleDate calendar = new SimpleDate();

            String[] stockDatas = respond.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // There must be at least two lines : header information and history information.
            int length = stockDatas.Count();

            if (length <= 1)
            {
                return false;
            }

            Symbol symbol = Symbol.newInstance(code.toString());
            String name = symbol.toString();
            Stock.Board board = Stock.Board.Unknown;
            Stock.Industry industry = Stock.Industry.Unknown;

            try
            {
                Stock stock = getStockServer(this.country).getStock(code);
                symbol = stock.getSymbol();
                name = stock.getName();
                board = stock.getBoard();
                industry = stock.getIndustry();
            }
            catch (StockNotFoundException exp)
            {
                log.Error(null, exp);
            }

            double previousClosePrice = Double.MaxValue;

            for (int i = length - 1; i > 0; i--)
            {
                // Use > instead of >=, to avoid header information (Date,Open,High,Low,Close,Volume,Adj Close)
                String[] fields = stockDatas[i].Split(',');

                // Date,Open,High,Low,Close,Volume,Adj Close
                if (fields.Count() < 7)
                {
                    continue;
                }

                try
                {
                    DateTime curcalendar = DateTime.ParseExact(fields[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    calendar = new SimpleDate(curcalendar);
                }
                catch (FormatException ex)
                {
                    log.Error(null, ex);
                    continue;
                }

                double prevPrice = 0.0;
                double openPrice = 0.0;
                double highPrice = 0.0;
                double lowPrice = 0.0;
                double closePrice = 0.0;
                // TODO: CRITICAL LONG BUG REVISED NEEDED.
                long volume = 0;
                //double adjustedClosePrice = 0.0;

                try
                {
                    prevPrice = (previousClosePrice == Double.MaxValue) ? 0 : previousClosePrice;
                    openPrice = Double.Parse(fields[1]);
                    highPrice = Double.Parse(fields[2]);
                    lowPrice = Double.Parse(fields[3]);
                    closePrice = Double.Parse(fields[4]);
                    // TODO: CRITICAL LONG BUG REVISED NEEDED.
                    volume = long.Parse(fields[5]);
                    //adjustedClosePrice = Double.Parse(fields[6]);
                }
                catch (FormatException exp)
                {
                    log.Error(null, exp);
                }

                double changePrice = (previousClosePrice == Double.MaxValue) ? 0 : closePrice - previousClosePrice;
                double changePricePercentage = ((previousClosePrice == Double.MaxValue) || (previousClosePrice == 0.0)) ? 0 : changePrice / previousClosePrice * 100.0;

                SimpleDate simpleDate = calendar;

                Stock stock = new Stock(
                        code,
                        symbol,
                        name,
                        board,
                        industry,
                        prevPrice,
                        openPrice,
                        closePrice, /* Last Price. */
                        highPrice,
                        lowPrice,
                        volume,
                        changePrice,
                        changePricePercentage,
                        0,
                        0.0,
                        0,
                        0.0,
                        0,
                        0.0,
                        0,
                        0.0,
                        0,
                        0.0,
                        0,
                        0.0,
                        0,
                        simpleDate
                        );

                historyDatabase.Add(simpleDate, stock);
                simpleDates.Add(simpleDate);
                previousClosePrice = closePrice;
            }

            return (historyDatabase.Count > 0);
        }

        private void buildHistory(Code code)
        {
            StringBuilder stringBuilder = new StringBuilder(YAHOO_ICHART_BASED_URL);

            String symbol = code.toString();

            stringBuilder.Append(symbol);

            int endMonth = duration.getEndDate().Month;
            int endDate = duration.getEndDate().Day;
            int endYear = duration.getEndDate().Year;
            int startMonth = duration.getStartDate().Month;
            int startDate = duration.getStartDate().Day;
            int startYear = duration.getStartDate().Year;

            StringBuilder formatBuilder = new StringBuilder("&d=");
            formatBuilder.Append(endMonth).Append("&e=").Append(endDate).Append("&f=").Append(endYear).Append("&g=d&a=").Append(startMonth).Append("&b=").Append(startDate).Append("&c=").Append(startYear).Append("&ignore=.csv");

            String location = stringBuilder.Append(formatBuilder).ToString();

            bool success = false;

            for (int retry = 0; retry < NUM_OF_RETRY; retry++)
            {
                String respond = Gui.Utils.getResponseBodyAsStringBasedOnProxyAuthOption(location);

                if (respond == null)
                {
                    continue;
                }

                success = parse(respond);

                if (success)
                {
                    break;
                }
            }

            if (success == false)
            {
                throw new StockHistoryNotFoundException(code.toString());
            }
        }

        public Stock getStock(SimpleDate calendar)
        {
            return historyDatabase[calendar];
        }

        public SimpleDate getCalendar(int index)
        {
            return simpleDates[index];
        }

        public int getNumOfCalendar()
        {
            return simpleDates.Count;
        }

        public long getSharesIssued()
        {
            return 0;
        }

        public long getMarketCapital()
        {
            return 0;
        }

        public Duration getDuration()
        {
            return duration;
        }

        // http://ichart.yahoo.com/table.csv?s=JAVA&d=10&e=14&f=2008&g=d&a=2&b=11&c=1987&ignore=.csv
        // d = end month (0-11)
        // e = end date
        // f = end year
        // g = daily?
        // a = start month (0-11)
        // b = start date
        // c = start year
        //
        // Date,Open,High,Low,Close,Volume,Adj Close
        // 2008-11-07,4.32,4.41,4.12,4.20,10882100,4.20
        // 2008-11-06,4.57,4.60,4.25,4.25,10717900,4.25
        // 2008-11-05,4.83,4.90,4.62,4.62,9250800,4.62

        private static int NUM_OF_RETRY = 2;
        private static Duration DEFAULT_HISTORY_DURATION = Duration.getTodayDurationByYears(10);
        private static String YAHOO_ICHART_BASED_URL = "http://ichart.yahoo.com/table.csv?s=";

        private Dictionary<SimpleDate, Stock> historyDatabase = new Dictionary<SimpleDate, Stock>();
        private List<SimpleDate> simpleDates = new List<SimpleDate>();

        private Code code;
        private Country country;
        private Duration duration;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
