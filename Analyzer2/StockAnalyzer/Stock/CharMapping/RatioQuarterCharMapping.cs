using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock.CharMapping
{
    class RatioQuarterCharMapping : StockHistoryCharMapping
    {
        protected override void Init()
        {
            List<string> chars = new List<string>();

            for (int i = -41; i < 42; i++)
            {
                string s = Convert.ToChar(i + 41 + 35).ToString(); // Start from ASCII 35 
                RatioToString_.Add(i, s);
                StringToRatio_.Add(s, i);
            }
        }

        protected override string GetRatioString(double ratio)
        {
            int r = (int)(ratio * 100 * 4);

            if (r > MAXRATIOQUARTER)
            {
                return RatioToString_[MAXRATIOQUARTER];
            }
            else if (r < MINRATIOQUARTER)
            {
                return RatioToString_[MINRATIOQUARTER];
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
            return 4;
        }

        const int MINRATIOQUARTER = -41;
        const int MAXRATIOQUARTER = 41;
    }
}
