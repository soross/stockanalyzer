using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DotNetStock.Adapter;
using System.Web;

namespace DotNetStock.Engine
{
    class YahooStockServer : AbstractYahooStockServer
    {
        protected override String getYahooCSVBasedURL()
        {
            return "http://finance.yahoo.com/d/quotes.csv?s=";
        }

        public YahooStockServer(Country param)
            : base(param)
        {
            this.baseURL = YahooStockServer.servers[param];

            InitServer();
        }

        public void InitServer()
        {
            servers.Add(Country.Australia, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=AX&sub=Look+Up"));
            servers.Add(Country.Austria, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=VI&sub=Look+Up"));
            servers.Add(Country.Belgium, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=BR&sub=Look+Up"));
            servers.Add(Country.Canada, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=TO&sub=Look+Up"));
            servers.Add(Country.Denmark, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=CO&sub=Look+Up"));
            servers.Add(Country.France, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=PA&sub=Look+Up"));
            servers.Add(Country.Germany, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=GER&sub=Look+Up"));
            servers.Add(Country.India, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=IN&sub=Look+Up"));
            servers.Add(Country.Italy, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=MI&sub=Look+Up"));
            servers.Add(Country.Netherlands, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=AS&sub=Look+Up"));
            servers.Add(Country.Norway, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=OL&sub=Look+Up"));
            servers.Add(Country.Portugal, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=LI&sub=Look+Up"));
            servers.Add(Country.Spain, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=ESP&sub=Look+Up"));
            servers.Add(Country.Sweden, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=ST&sub=Look+Up"));
            servers.Add(Country.Switzerland, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=SWI&sub=Look+Up"));
            servers.Add(Country.UnitedKingdom, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=L&sub=Look+Up"));
            servers.Add(Country.UnitedState, new URL("http://uk.finsearch.yahoo.com/uk/index.php?s=uk_sort&nm=**&tp=S&r=US&sub=Look+Up"));
        }

        // The returned URLs, shouldn't have any duplication with visited,
        // and they are unique. Although is more suitable that we use Set,
        // use List is more convinient for us to iterate.
        private List<URL> getURLs(String respond, List<URL> visited)
        {
            List<URL> urls = new List<URL>();

            MatchCollection matcher = urlPattern.Matches(respond);

            for (int i = 0; i < matcher.Count; i++)
            {
                String str = matcher[i].Value;

                try
                {
                    URL url = new URL(baseURL, str);

                    if ((urls.Contains(url) == false) && (visited.Contains(url) == false))
                    {
                        urls.Add(url);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("", ex);
                }
            }

            return urls;
        }


        public override List<Stock> getAllStocks()
        {
            if (this.baseURL == null)
            {
                // For certain countries, we have no intention to getAllStocks
                // through Yahoo service. In that case, the baseURL will simply
                // be null.
                throw new StockNotFoundException(this.getCountry().ToString());
            }
            List<URL> visited = new List<URL>();

            List<Stock> stocks = new List<Stock>();

            // Use Set, for safety purpose to avoid duplication.
            HashSet<Code> codes = new HashSet<Code>();

            visited.Add(baseURL);

            for (int i = 0; i < visited.Count; i++)
            {
                String location = visited[i].toString();

                String respond = Gui.Utils.getResponseBodyAsStringBasedOnProxyAuthOption(location);

                if (respond == null)
                {
                    continue;
                }

                List<Stock> tmpStocks = getStocks(respond);
                List<URL> urls = getURLs(respond, visited);

                foreach (Stock stock in tmpStocks)
                {
                    if (codes.Add(stock.getCode()))
                    {
                        stocks.Add(stock);
                    }
                }

                foreach (URL url in urls)
                {
                    if (visited.Contains(url))
                    {
                        continue;
                    }
                    visited.Add(url);
                }

                //notify(this, stocks.Count);
            }

            return stocks;
        }

        private List<Stock> getStocks(String respond)
        {
            List<Stock> stocks = new List<Stock>();
            // In order to avoid duplicated stock's code.
            HashSet<Code> codes = new HashSet<Code>();

            // A hacking way to prevent
            // <a href="http://uk.finance.yahoo.com/q?s=SCRIA.ST+NCCA.ST+SHBB.ST+ELUXA.ST+LATOA.ST+VOLVA.ST+SCVA.ST+SKFA.ST+SEBC.ST+DV-BTA-1.ST+MTGB.ST+63054.ST+37341.ST+HOLMA.ST+TEL2A.ST+62893.ST+RATOA.ST+585671.ST+">View Quotes for All Above Symbols</a>
            // going into our database. Although we had placed some restriction on the
            // symbol maximum length, it may be some exceptional case sometimes.
            // For example : <a href="http://uk.finance.yahoo.com/q?s=SCRIA.ST+NCCA.ST+">View Quotes for All Above Symbols</a>
            Regex nonSymbolPattern = new Regex("View Quotes for All Above Symbols", RegexOptions.IgnoreCase);

            var matcher = stockAndBoardPattern.Matches(respond);
            for (int i = 1; i <= matcher.Count; i += 3)
            {
                String c = matcher[i].Value;
                if (c == null)
                {
                    continue;
                }

                String s = matcher[i + 1].Value;
                if (s == null)
                {
                    continue;
                }

                String b = matcher[i + 2].Value;
                if (b == null)
                {
                    continue;
                }

                c = c.Trim();
                s = s.Trim();

                // Change Parken Sport &amp; Entertainment A/S to
                // Parken Sport & Entertainment A/S
                s = HttpUtility.HtmlDecode(s);

                if (nonSymbolPattern.IsMatch(s))
                {
                    continue;
                }

                // Enum name is not allowed to have space in between.
                b = b.Trim().Replace("\\s+", "");

                Code code = Code.newInstance(c);
                Symbol symbol = Symbol.newInstance(s);
                Stock.Board board = Stock.Board.Unknown;

                if (codes.Add(code) == false)
                {
                    // The stock with same code had been added previously. We
                    // do not want any stock with duplicated code.
                    continue;
                }

                // Since '-' character is not allowed in enum, we need some sort
                // of hacking.
                if (b == "Virt-X")
                {
                    b = "Virt_X";
                }

                try
                {
                    board = (Stock.Board)Enum.Parse(typeof(Stock.Board), b);
                }
                catch (ArgumentException exp)
                {
                    log.Error(null, exp);
                    board = Stock.Board.Unknown;
                }

                Stock stock = new Stock(
                       code,
                       symbol,
                       "",
                       board,
                       Stock.Industry.Unknown,
                       0.0,
                       0.0,
                       0.0,
                       0.0,
                       0.0,
                       0,
                       0.0,
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
                       0.0,
                       0,
                       Calendar.getInstance()
                       );
                stocks.Add(stock);
            }

            return stocks;
        }

        private URL baseURL;

        private static Dictionary<Country, URL> servers = new Dictionary<Country, URL>();
        private static Regex urlPattern = new Regex("<a\\s+href\\s*=\\s*\"?(/uk/index.php.+?)\"?>", RegexOptions.Compiled);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /*
        <tr>
        <td nowrap="" class="yfnc_h">
         <a href="http://uk.finance.yahoo.com/q?s=VWS.CO">Vestas Wind Systems A/S</a>
        </td>
        <td nowrap="" align="center" class="yfnc_h">
         VWS.CO</td>
        <td nowrap="" align="center" class="yfnc_h">
         DK0010268606</td>
        <td nowrap="" align="center" class="yfnc_h">
         Copenhagen</td>
        <td nowrap="" align="center" class="yfnc_h">
         Stock</td>
        <td nowrap="" align="center" class="yfnc_h">
         <b>269.50<small>DKK</small> </b> <small>11:50</small></td>
        <td nowrap="" align="center" class="yfnc_h">
           <b style="color: rgb(204, 0, 0);"> -4.09%</b> </td>
        <td nowrap="" align="right" class="yfnc_h">
         1,373,728</td>
        <td nowrap="" align="center" class="yfnc_h">
         <img height="10" border="0" width="10" src="http://us.i1.yimg.com/us.yimg.com/i/us/fi/gr/track_trns_1.gif"/>
        <a href="http://uk.finedit.yahoo.com/ec?.intl=uk&amp;.src=quote&amp;.portfover=1&amp;.done=http://uk.finance.yahoo.com/&amp;.cancelPage=http://uk.biz.yahoo.com/quote_lookup.html&amp;.sym=VWS.CO&amp;.nm=Vestas+Wind+Systems+A%2FS">Add</a>
        </td>
        </tr>
         */
        // <a href="http://uk.finance.yahoo.com/q?s=RBS.L">Royal Bank Of Scotland</a>
        // First group is stock code, second group is stock symbol, 3rd group is board.
        // Board information is located at the fourth column of the table.
        // Pattern.DOTALL means for '.', we want to match everything including newline.
        //
        // Take note that, we need to avoid this kind of URLs sometimes :
        // <font face="arial" size="-1"><a href="http://uk.finance.yahoo.com/q?s=SCRIA.ST+NCCA.ST+SHBB.ST+ELUXA.ST+LATOA.ST+VOLVA.ST+SCVA.ST+SKFA.ST+SEBC.ST+DV-BTA-1.ST+MTGB.ST+63054.ST+37341.ST+HOLMA.ST+TEL2A.ST+62893.ST+RATOA.ST+585671.ST+">View Quotes for All Above Symbols</a></font>
        // We use "Limiting Repetition" to distinguish them out of normal stock code. We
        // assume the longest stock code length will <= 64.
        private static Regex stockAndBoardPattern = new Regex("<a\\s+href\\s*=[^>]+s=([^\">]{1,64})\"?>(.+?)</a>.*?</td>.*?<td[^>]*>.*?</td>.*?<td[^>]*>.*?</td>.*?<td[^>]*>(.*?)</td>", RegexOptions.Compiled);
    }
}