using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Business
{
    class BonusInfo
    {
        // 分红金额
        public double Dividend
        {
            get;
            set;
        }

        // 送股数目
        public int BonusCount
        {
            get;
            set;
        }
    }
}
