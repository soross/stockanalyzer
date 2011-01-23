using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer
{
    public class StockHolder : IStockHolder
    {
        public StockHolder()
        {
        }

        public void BuyStock(int count, double unitPrice)
        {
            _CurrentStock.Buy(count, unitPrice);
        }

        public void AddBonusStock(int count)
        {
            BuyStock(count, 0);
        }

        public bool HasStock()
        {
            return _CurrentStock.Count > 0;
        }

        public int StockCount()
        {
            return _CurrentStock.Count;
        }

        public IStockHistory History
        {
            get
            {
                return _StockHistory;
            }
            set
            {
                _StockHistory = value;
            }
        }

        // 返回值为获得的金额
        public double SellStock(int count, double unitPrice)
        {
            // 卖出股票
            if (_CurrentStock.Count == 0)
            {
                return 0;
            }

            double price = _CurrentStock.Sell(count, unitPrice);
            double cash = price - Transaction.GetDutyCharge(price);

            LogMgr.Logger.LogInfo("Action: Sell Stock Count: {0}, Unit Price: {1}",
                    count,
                    unitPrice);
            return cash;
        }

        // 股票市值
        public double MarketValue(DateTime day)
        {
            IStockData stockProp = History.GetStock(day);
            if (stockProp == null)
            {
                stockProp = History.GetPrevDayStock(day);
            }

            if (stockProp != null)
            {
                return _CurrentStock.Count * stockProp.EndPrice;
            }
            else
            {
                return 0;
            }
        }

        // 单位成本
        public double UnitPrice
        {
            get
            {
                if (_CurrentStock.Count == 0)
                {
                    throw new NotSupportedException();
                }

                return _CurrentStock.UnitPrice;
            }
        }

        Stock _CurrentStock = new Stock();
        private IStockHistory _StockHistory;
    }
}
