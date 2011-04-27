using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Common.Data
{
    public class Bonus
    {
        public int BonusYear
        {
            get { return _BonusYear; }
            set { _BonusYear = value; }
        }

        public int StockId
        {
            get { return _stockId; }
            set { _stockId = value; }
        }

        // 每股送股比例
        public double BonusCount
        {
            get { return _bonusCount; }
            set { _bonusCount = value; }
        }

        // 每股派现金额
        public double Dividend
        {
            get { return _dividend; }
            set { _dividend = value; }
        }

        // 股权登记日
        public DateTime RegistOn
        {
            get { return _RegistOn; }
            set { _RegistOn = value; }
        }

        // 除权除息日
        public DateTime ExexDividend
        {
            get { return _ExexDividend; }
            set { _ExexDividend = value; }
        }

        // 派息日
        public DateTime DividendDate
        {
            get { return _DividendDate; }
            set { _DividendDate = value; }
        }

        // 红股上市日
        public DateTime BonusListOn
        {
            get { return _BonusListOn; }
            set { _BonusListOn = value; }
        }

        int _stockId;
        double _bonusCount; // 每股送股比例
        double _dividend; // 每股派现金额
        int _BonusYear; // 分红年份
                
        DateTime _RegistOn; // 股权登记日
        DateTime _ExexDividend; // 除权除息日
        DateTime _DividendDate; // 派息日
        DateTime _BonusListOn; // 红股上市日
    }
}
