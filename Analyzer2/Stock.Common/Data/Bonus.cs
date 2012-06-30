using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Common.Data
{
    public class Bonus
    {
        public int BonusYear
        {
            get { return BonusYear_; }
            set { BonusYear_ = value; }
        }

        public int StockId
        {
            get { return stockId_; }
            set { stockId_ = value; }
        }

        // 每股送股比例
        public double BonusCount
        {
            get { return bonusCount_; }
            set { bonusCount_ = value; }
        }

        // 每股派现金额
        public double Dividend
        {
            get { return dividend_; }
            set { dividend_ = value; }
        }

        // 股权登记日
        public DateTime RegistOn
        {
            get { return RegistOn_; }
            set { RegistOn_ = value; }
        }

        // 除权除息日
        public DateTime ExexDividend
        {
            get { return ExexDividend_; }
            set { ExexDividend_ = value; }
        }

        // 派息日
        public DateTime DividendDate
        {
            get { return DividendDate_; }
            set { DividendDate_ = value; }
        }

        // 红股上市日
        public DateTime BonusListOn
        {
            get { return BonusListOn_; }
            set { BonusListOn_ = value; }
        }

        int stockId_;
        double bonusCount_; // 每股送股比例
        double dividend_; // 每股派现金额
        int BonusYear_; // 分红年份
                
        DateTime RegistOn_; // 股权登记日
        DateTime ExexDividend_; // 除权除息日
        DateTime DividendDate_; // 派息日
        DateTime BonusListOn_; // 红股上市日
    }
}
