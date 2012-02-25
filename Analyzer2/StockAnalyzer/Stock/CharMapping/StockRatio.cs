using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock.CharMapping
{
    public class StockRatio
    {
        public double UpRatio
        {
            get;
            set;
        }

        public double RiseRatio
        {
            get;
            set;
        }

        public double MaxRatio
        {
            get;
            set;
        }

        public double MinRatio
        {
            get;
            set;
        }

        public string GetDescription()
        {
            return "Rise (%): " + RiseRatio + ", Up (%): " + UpRatio + ", Max (%): " + MaxRatio + ", Min (%): " + MinRatio;
        }
    }
}
