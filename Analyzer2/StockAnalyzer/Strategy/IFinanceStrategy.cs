using System;
using System.Collections.Generic;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy
{
    /// <summary>
    /// Ͷ�ʲ���
    /// ����ÿ��Ĺ�Ʊ��Ϣ���������������
    /// </summary>
    public abstract class IFinanceStrategy
    {
        /// <summary>
        /// �õ�����Ĳ���
        /// </summary>
        /// <param name="day">ָ������</param>
        /// <param name="account">�˻�</param>
        /// <returns>����Ĳ���</returns>
        public abstract ICollection<StockOper> GetOper(DateTime day, IAccount account);

        /// <summary>
        /// Ͷ�ʲ�������
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
