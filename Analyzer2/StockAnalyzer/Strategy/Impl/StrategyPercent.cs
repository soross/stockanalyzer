using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    public class StrategyPercent : IFinanceStrategy
    {
        public StrategyPercent(double percent)
        {
            winPercent = percent;
        }

        // ָ��Ϊǰһ�����ֵ���룬׬x%����
        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData prevStockProp = stockHistory.GetPrevDayStock(day);
            if (prevStockProp == null)
            {
                Debug.WriteLine("StrategyPercent -- GetPrevDayStock ERROR: Cur Day: " + day.ToLongDateString());
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    prevStockProp.MinPrice);
            if (stockCount > 0)
            {
                StockOper oper = new StockOper(prevStockProp.MinPrice, stockCount, OperType.Buy);
                opers.Add(oper);
            }

            if (stockHolder.HasStock())
            {
                double unitCost = stockHolder.UnitPrice;
                if (unitCost > 0)
                {
                    StockOper oper2 = new StockOper(unitCost * (1 + winPercent), stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper2);
                }
            }

            return opers;
        }

        public override string Name
        {
            get
            {
                return "Percent: " + Profit.ToString("F03", CultureInfo.CurrentCulture);
            }
        }

        // ӯ���İٷֱ�
        public double Profit
        {
            get
            {
                return winPercent;
            }
            set
            {
                winPercent = value;
            }
        }

        private double winPercent;
    }
}
