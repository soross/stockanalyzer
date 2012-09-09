using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock.CharMapping
{
    public class RatioTenthCharMapping : StockHistoryCharMapping
    {
        public RatioTenthCharMapping(double step)
        {
            StepChar_ = step;

            maxRatio_ = (int)(10.0 / StepChar_);
            minRatio_ = -maxRatio_;
        }

        protected override void Init()
        {
        }

        public override string GetRatioString(double ratio)
        {
            double val = ((ratio * 100) / StepChar_);
            int r = (int)Math.Round(val);            

            delta_ = maxRatio_;

            if (r > maxRatio_)
            {
                return STR_MAPPING.ElementAt(maxRatio_ + delta_).ToString();
            }
            else if (r < minRatio_)
            {
                return STR_MAPPING.ElementAt(minRatio_ + delta_).ToString();
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

        int maxRatio_;
        int minRatio_;
        int delta_;
        const string STR_MAPPING = "KJIHGFEDCBAabcdefghijk";

        // 两个字符代表的间隔
        double StepChar_;
    }
}
