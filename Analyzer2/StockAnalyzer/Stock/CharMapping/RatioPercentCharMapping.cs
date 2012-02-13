using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock.CharMapping
{
    class RatioPercentCharMapping : StockHistoryCharMapping
    {
        protected override void Init()
        {
            List<string> chars = new List<string>();
            for (char j = 'a'; j < 'z'; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    string s = string.Format("{0}{1}", j.ToString(), i);
                    chars.Add(s);
                }
            }

            for (int i = -101; i < 102; i++)
            {
                RatioToString_.Add(i, chars[i + 101]);
                StringToRatio_.Add(chars[i + 101], i);
            }
        }

        protected override string GetRatioString(double ratio)
        {
            int r = (int)(ratio * 1000) + 101;

            if (r > MAXRATIO)
            {
                return RatioToString_[MAXRATIO];
            }
            else if (r < MINRATIO)
            {
                return RatioToString_[MINRATIO];
            }
            else
            {
                return RatioToString_[r];
            }
        }

        public override StockRatio ParseRatio(string s)
        {
            throw new NotImplementedException();
        }

        public override int stockDayStringLength()
        {
            return 8;
        }

        const int MINRATIO = -101;
        const int MAXRATIO = 101;
    }
}
