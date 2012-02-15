using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock.CharMapping
{
    class RatioTenthCharMapping : StockHistoryCharMapping
    {
        public RatioTenthCharMapping(double step)
        {
            StepChar_ = step;
        }

        protected override void Init()
        {
        }

        protected override string GetRatioString(double ratio)
        {
            int r = (int)((ratio * 100) / StepChar_);
            int maxRatio = (int)(10.0 / StepChar_);
            int minRatio = -maxRatio;
            delta_ = maxRatio;

            if (r > maxRatio)
            {
                return STR_MAPPING.ElementAt(maxRatio + delta_).ToString();
            }
            else if (r < minRatio)
            {
                return STR_MAPPING.ElementAt(minRatio + delta_).ToString();
            }
            else
            {
                return STR_MAPPING.ElementAt(r + delta_).ToString();
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

        double CharToRatio(Char c)
        {
            int pos = STR_MAPPING.IndexOf(c);
            if (pos != -1)
            {
                return ((pos - delta_) / 100.0) * StepChar_;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override int stockDayStringLength()
        {
            return 4;
        }

        int delta_;
        const string STR_MAPPING = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // 两个字符代表的间隔
        double StepChar_;
    }
}
