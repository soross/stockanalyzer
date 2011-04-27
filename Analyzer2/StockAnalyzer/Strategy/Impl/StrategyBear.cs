using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Impl
{
    public class StrategyBear : IFinanceStrategy
    {
        // 指令为如果没有股票，当日开盘买入，否则涨跌幅达到x%卖出（止盈止损策略）
        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData prevStockProp = stockHistory.GetPrevDayStock(day);
            IStockData curStockProp = stockHistory.GetStock(day);
            if ((prevStockProp == null) || (curStockProp == null))
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();

            if (!stockHolder.HasStock())
            {                
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                        curStockProp.StartPrice); // 如果没有股票，当天开盘买入
                if (stockCount > 0)
                {
                    StockOper oper = new StockOper(curStockProp.StartPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                    return opers; // 有疑问，目前不支持一天之内的买卖
                }
            }            
            else
            {
                double unitCost = stockHolder.UnitPrice;
                if (unitCost > 0)
                {
                    if (curStockProp.MaxPrice >= (unitCost * (1 + winPercent)))
                    {
                        // 止盈
                        StockOper oper2 = new StockOper(unitCost * (1 + winPercent), stockHolder.StockCount(), OperType.Sell);
                        opers.Add(oper2);
                        return opers;
                    }
                    
                    if (curStockProp.MinPrice <= (unitCost * (1 - winPercent)))
                    {
                        // 止损
                        StockOper oper1 = new StockOper(unitCost * (1 - winPercent), stockHolder.StockCount(), OperType.Sell);
                        opers.Add(oper1);
                        return opers;
                    }                    
                }
            }

            return null;
        }

        public override string Name
        {
            get
            {
                return "Bear: " + Profit.ToString("F03", CultureInfo.CurrentCulture);
            }
        }

        // 盈利或亏损的百分比
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
