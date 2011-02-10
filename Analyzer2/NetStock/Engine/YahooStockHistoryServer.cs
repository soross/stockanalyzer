
namespace DotNetStock.Engine
{
    class YahooStockHistoryServer : AbstractYahooStockHistoryServer
    {
        public YahooStockHistoryServer(Country country, Code code)
            : base(country, code)
        {
        }

        public YahooStockHistoryServer(Country country, Code code, Duration duration)
            : base(country, code, duration)
        {
        }

        protected override StockServer getStockServer(Country country)
        {
            return new YahooStockServer(country);
        }
    }
}
