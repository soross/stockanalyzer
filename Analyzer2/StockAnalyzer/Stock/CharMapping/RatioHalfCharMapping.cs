using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock.CharMapping
{
    class RatioHalfCharMapping : StockHistoryCharMapping
    {
        protected override void Init()
        {
        }

        protected override string GetRatioString(double ratio)
        {
            int r = (int)(ratio * 100 * 2);

            if (r > MAXRATIO)
            {
                return STR_MAPPING.ElementAt(MAXRATIO + DELTA).ToString();
            }
            else if (r < MINRATIO)
            {
                return STR_MAPPING.ElementAt(MINRATIO + DELTA).ToString();
            }
            else
            {
                return STR_MAPPING.ElementAt(r + DELTA).ToString();
            }
        }

        const int DELTA = 21;
        const int MINRATIO = -21;
        const int MAXRATIO = 21;

        const string STR_MAPPING = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
