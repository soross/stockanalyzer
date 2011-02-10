using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class StockEx : Stock
    {
        public StockEx(Stock stock, long marketCapital, long sharesIssued)
            : base(stock)
        {
            this.marketCapital = marketCapital;
            this.sharesIssued = sharesIssued;

        }

        public long getSharesIssued()
        {
            return sharesIssued;
        }

        public long getMarketCapital()
        {
            return marketCapital;
        }

        private long sharesIssued;
        private long marketCapital;
    }
}
