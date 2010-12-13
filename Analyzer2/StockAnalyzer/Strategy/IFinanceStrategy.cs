using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer.Strategy
{
    // Í¶×Ê²ßÂÔ
    public abstract class IFinanceStrategy
    {
        public abstract ICollection<StockOper> GetOper(DateTime day, IAccount account);

        public abstract string Name
        {
            get;
        }

        public IStockHolder Holder
        {            
            set
            {
                _StockHolder = value;
                _StockHistory = _StockHolder.History;
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

        protected IStockHolder _StockHolder;
        protected IStockHistory _StockHistory;
    }    
}
