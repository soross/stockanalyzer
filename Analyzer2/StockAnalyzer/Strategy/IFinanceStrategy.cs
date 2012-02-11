using System;
using System.Collections.Generic;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy
{
    /// <summary>
    /// 投资策略
    /// 根据每天的股票信息决策买入或者卖出
    /// </summary>
    public abstract class IFinanceStrategy
    {
        /// <summary>
        /// 得到当天的操作
        /// </summary>
        /// <param name="day">指定日期</param>
        /// <param name="account">账户</param>
        /// <returns>当天的操作</returns>
        public abstract ICollection<StockOper> GetOper(DateTime day, IAccount account);

        /// <summary>
        /// 投资策略名称
        /// </summary>
        public abstract string Name
        {
            get;
        }

        public IStockHolder Holder
        {            
            set
            {
                stockHolder = value;
                stockHistory = stockHolder.History;
                HolderInit();
            }
        }

        protected virtual void HolderInit()
        {
        }

        protected static bool CheckStock(IStockData stock, DateTime day)
        {
            if (stock == null)
            {
                StrategyLog.DebugFormat("ERROR: Cannot find Stock Property of date: {0}!", day.ToShortDateString());
                return false;
            }
            else
            {
                return true;
            }
        }

        // Create a logger for use in this class
        protected static readonly log4net.ILog StrategyLog = log4net.LogManager.GetLogger(typeof(IFinanceStrategy));

        protected IStockHolder stockHolder;
        protected IStockHistory stockHistory;
    }    
}
