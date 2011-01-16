using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Impl
{
    // 竹节投资法，涨10%则卖出10%，跌10%则买进10% 
    class StrategyBamboo : IFinanceStrategy
    {
        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curStockProp = stockHistory.GetStock(day);
            if (curStockProp == null)
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();

            if (!stockHolder.HasStock())
            {
                // 无股票则买入10% 
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll * BAMBOOPERCENT,
                        curStockProp.StartPrice); // 开盘买入 
                if (stockCount >= STOCKHAND)
                {
                    StockOper oper = new StockOper(curStockProp.StartPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                }
            }
            else
            {
                double unitCost = stockHolder.UnitPrice; // 持仓成本
                if (unitCost > 0)
                {
                    double expectedMinPrice = unitCost * (1 - BAMBOOPERCENT);
                    double expectedMaxPrice = unitCost * (1 + BAMBOOPERCENT);

                    if (curStockProp.MinPrice <= expectedMinPrice)
                    {
                        // 现金的10% 
                        int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll * BAMBOOPERCENT,
                                curStockProp.StartPrice);

                        if (stockCount > 0)
                        {
                            StockOper oper2 = new StockOper(expectedMinPrice, stockCount, OperType.Buy);
                            opers.Add(oper2);
                        }                        
                    }

                    if (curStockProp.MaxPrice >= expectedMaxPrice)
                    {
                        // 卖出持仓股票的10%
                        double sellCount = stockHolder.StockCount() * BAMBOOPERCENT;
                        if (sellCount >= STOCKHAND)
                        {
                            opers.Add(new StockOper(expectedMaxPrice, Convert.ToInt32(sellCount), OperType.Sell));
                        }                        
                    }
                }
            }

            return opers;
        }

        public override string Name
        {
            get { return "Bamboo"; }
        }

        private const double BAMBOOPERCENT = 0.1;
        private const int STOCKHAND = 100; // 每手股数 
    }
}
