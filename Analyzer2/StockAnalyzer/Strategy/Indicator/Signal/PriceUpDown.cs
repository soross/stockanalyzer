using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class PriceUpDown
    {
        public double Price
        {
            get;
            set;
        }
        public bool IsUp
        {
            get;
            set;
        }

        public PriceUpDown(double price, bool isUp)
        {
            Price = price;
            IsUp = isUp;
        }
    };
}
