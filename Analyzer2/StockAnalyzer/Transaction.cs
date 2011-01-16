using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer
{
    // 交易
    public sealed class Transaction
    {
        // 手续费
        private const double CommissionChargeRate = 0.0025;

        // 印花税
        private const double StampDutyRate = 0.001;

        // 得到可以购买的股票数目
        public static int GetCanBuyStockCount(double bankRoll, double unitPrice)
        {
            double count = bankRoll / ((unitPrice) * (1 + CommissionChargeRate + StampDutyRate));

            double remain = count - (count % 100);
            return (int)remain;
        }

        // 股票交易一次的总成本
        public static double GetTotalCharge(double tradingVolume)
        {
            return tradingVolume + GetDutyCharge(tradingVolume);
        }

        // 股票交易一次的税费成本
        public static double GetDutyCharge(double tradingVolume)
        {
            return (tradingVolume * TotalChargeRate());
        }

        // 股票交易一次的成本占交易额的比例 
        public static double TotalChargeRate()
        {
            return CommissionChargeRate + StampDutyRate;
        }

        private Transaction()
        {
        }
    }
}
