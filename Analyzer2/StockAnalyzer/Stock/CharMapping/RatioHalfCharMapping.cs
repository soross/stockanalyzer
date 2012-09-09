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

        public override string GetRatioString(double ratio)
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

        public override StockRatio ParseRatio(string s)
        {
            if (s.Length != 4)
            {
                throw new ArgumentException();
            }

            StockRatio ratio = new StockRatio();
            char[] arr = s.ToCharArray();

            ratio.RiseRatio = CharToRatio(arr[0]);

            ratio.UpRatio = CharToRatio(arr[1]);

            ratio.MaxRatio = CharToRatio(arr[2]);

            ratio.MinRatio = CharToRatio(arr[3]);

            return ratio;
        }

        public override int stockDayStringLength()
        {
            return 4;
        }

        double CharToRatio(Char c)
        {
            int pos = STR_MAPPING.IndexOf(c);
            if (pos != -1)
            {
                return ((pos - DELTA) / (2 * 100.0));
            }
            else
            {
                throw new ArgumentException();
            }
        }

        const int DELTA = 21;
        const int MINRATIO = -21;
        const int MAXRATIO = 21;

        const string STR_MAPPING = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
