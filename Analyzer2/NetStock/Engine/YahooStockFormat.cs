using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DotNetStock.Engine
{
    class YahooStockFormat : StockFormat
    {
        private YahooStockFormat() { }

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
        //                          in between two string literal (which will always Contains "). By using regular
        //                          expression, we will manually remove the comma.
        // c1 = Change
        // p2 = Change Percent
        // k3 = Last Trade Size <-- We need to take special care on this, it may give us 1,234...
        // b = Bid
        // b6 = Bid Size        <-- We need to take special care on this, it may give us 1,234...
        // a = Ask
        // a5 = Ask Size        <-- We need to take special care on this, it may give us 1,234...
        // d1 = Last Trade Date
        // t1 = Last Trade Time
        //
        // c6k2c1p2c -> Change (Real-time), Change Percent (Real-time), Change, Change in Percent, Change & Percent Change
        // "+1400.00","N/A - +4.31%",+1400.00,"+4.31%","+1400.00 - +4.31%"
        //
        // "MAERSKB.CO","AP MOELLER-MAERS-","Copenhagen",32500.00,33700.00,34200.00,33400.00,660,"+1200.00","N/A - +3.69%",33,33500.00,54,33700.00,96,"11/10/2008","10:53am"    

        public List<Stock> parse(String source)
        {
            List<Stock> stocks = new List<Stock>();

            if (source == null)
            {
                return stocks;
            }

            String[] strings = source.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (String str in strings)
            {

                ///  String tmp = YahooStockFormat.digitPattern.matcher(string).replaceAll("$1");
                var tmp = YahooStockFormat.digitPattern.Matches(str);
                string strtmp = YahooStockFormat.digitPattern.Match(str).Value;
                foreach (var item in tmp)
                {
                    // ??
                }

                // Some string contain comma, remove them as well. If not, we face problem during csv parsing.
                String stringDigitWithoutComma = stringCommaPattern.Match(strtmp).Value;

                String[] fields = stringDigitWithoutComma.Split(',');
                int length = fields.Count();

                Code code = null;
                Symbol symbol = null;
                String name = null;
                Stock.Board board = Stock.Board.Unknown;
                Stock.Industry industry = Stock.Industry.Unknown;
                double prevPrice = 0.0;
                double openPrice = 0.0;
                double lastPrice = 0.0;
                double highPrice = 0.0;
                double lowPrice = 0.0;
                // TODO: CRITICAL LONG BUG REVISED NEEDED.
                long volume = 0;
                double changePrice = 0.0;
                double changePricePercentage = 0.0;
                int lastVolume = 0;
                double buyPrice = 0.0;
                int buyQuantity = 0;
                double sellPrice = 0.0;
                int sellQuantity = 0;
                double secondBuyPrice = 0.0;
                int secondBuyQuantity = 0;
                double secondSellPrice = 0.0;
                int secondSellQuantity = 0;
                double thirdBuyPrice = 0.0;
                int thirdBuyQuantity = 0;
                double thirdSellPrice = 0.0;
                int thirdSellQuantity = 0;
                DateTime calendar = DateTime.Today;

                do
                {
                    if (length < 1)
                        break;
                    code = Code.newInstance(quotePattern.Match(fields[0]).Value.Trim());

                    if (length < 2)
                        break;
                    name = quotePattern.Match(fields[1]).Value.Trim();

                    // We use name as symbol, to make it more readable.
                    symbol = Symbol.newInstance(name);

                    if (length < 3)
                        break;

                    try
                    {
                        board = (Stock.Board)Enum.Parse(typeof(Stock.Board), quotePattern.Match(fields[2]).Value.Trim());
                    }
                    catch (Exception exp)
                    {
                        board = Stock.Board.Unknown;
                    }

                    industry = Stock.Industry.Unknown;

                    if (length < 5)
                        break;
                    try
                    {
                        prevPrice = Double.Parse(fields[4]);
                    }
                    catch (FormatException exp)
                    { }

                    if (length < 7) break;
                    try { openPrice = Double.Parse(fields[6]); }
                    catch (FormatException exp) { }

                    if (length < 9) break;
                    try { lastPrice = Double.Parse(fields[8]); }
                    catch (FormatException exp) { }

                    if (length < 11) break;
                    try { highPrice = Double.Parse(fields[10]); }
                    catch (FormatException exp) { }

                    if (length < 13) break;
                    try { lowPrice = Double.Parse(fields[12]); }
                    catch (FormatException exp) { }

                    if (length < 15) break;
                    // TODO: CRITICAL LONG BUG REVISED NEEDED.
                    try { volume = long.Parse(fields[14]); }
                    catch (FormatException exp) { }

                    if (length < 17) break;
                    try { changePrice = Double.Parse(quotePattern.Match(fields[16]).Value.Trim()); }
                    catch (FormatException exp) { }

                    if (length < 19) break;
                    String _changePricePercentage = quotePattern.Match(fields[18]).Value;
                    _changePricePercentage = percentagePattern.Match(_changePricePercentage).Value;
                    try { changePricePercentage = Double.Parse(_changePricePercentage); }
                    catch (FormatException exp) { }

                    if (length < 21) break;
                    try { lastVolume = int.Parse(fields[20]); }
                    catch (FormatException exp) { }

                    if (length < 23) break;
                    try { buyPrice = Double.Parse(fields[22]); }
                    catch (FormatException exp) { }

                    if (length < 25) break;
                    try { buyQuantity = int.Parse(fields[24]); }
                    catch (FormatException exp) { }

                    if (length < 27) break;
                    try { sellPrice = Double.Parse(fields[26]); }
                    catch (FormatException exp) { }

                    if (length < 29) break;
                    try { sellQuantity = int.Parse(fields[28]); }
                    catch (FormatException exp) { }

                    if (length < 32) break;
                    String data_and_time = quotePattern.Match(fields[28]).Value.Trim() + " " + quotePattern.Match(fields[29]).Value.Trim();
                    DateTime serverDate;
                    try
                    {
                        serverDate = DateTime.ParseExact(data_and_time, "MM/dd/yyyy hh:mmaa", CultureInfo.InvariantCulture);
                        calendar = serverDate;
                    }
                    catch (FormatException exp)
                    {
                        // Most of the time, we just obtain "N/A"
                        // log.error(fields[23] + ", " + fields[24] + ", " + data_and_time, exp);
                    }

                    break;
                } while (true);

                if (code == null || symbol == null || name == null)
                {
                    continue;
                }

                if (calendar == null)
                {
                    calendar = DateTime.Now;
                }

                Stock stock = new Stock(
                        code,
                        symbol,
                        name,
                        board,
                        industry,
                        prevPrice,
                        openPrice,
                        lastPrice,
                        highPrice,
                        lowPrice,
                        volume,
                        changePrice,
                        changePricePercentage,
                        lastVolume,
                        buyPrice,
                        buyQuantity,
                        sellPrice,
                        sellQuantity,
                        secondBuyPrice,
                        secondBuyQuantity,
                        secondSellPrice,
                        secondSellQuantity,
                        thirdBuyPrice,
                        thirdBuyQuantity,
                        thirdSellPrice,
                        thirdSellQuantity,
                        calendar
                        );

                stocks.Add(stock);
            }

            return stocks;
        }

        public static StockFormat getInstance()
        {
            return stockFormat;
        }

        private static StockFormat stockFormat = new YahooStockFormat();
        // Used to remove the comma within an integer digit. The digit must be located
        // in between two string. Replaced with $1.
        //
        // digitPattern will change
        // ",100,000,"
        // to
        // ",100000,"
        private static Regex digitPattern = new Regex("(\",)|,(?=[\\d,]+,\")", RegexOptions.Compiled);
        // stringCommaPattern will change
        // ","abc,def","
        // to
        // ","abcdef","
        private static Regex stringCommaPattern = new Regex("(\",\")|,(?=[^\"[,]]*\",\")", RegexOptions.Compiled);
        private static Regex quotePattern = new Regex("\"", RegexOptions.Compiled);
        private static Regex percentagePattern = new Regex("%", RegexOptions.Compiled);
    }
}
