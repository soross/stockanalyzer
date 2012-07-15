using System;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// 保存一只股票的每天的信息
    /// </summary>
    public interface IStockHistory
    {
        DateTime MaxDate
        {
            get;
        }
        DateTime MinDate
        {
            get;
        }

        /// <summary>
        /// 股票代码
        /// </summary>
        int StockId
        {
            get;
        }
        
        // 得到某一天的股票属性
        IStockData GetStock(DateTime dt);

        IStockData GetPrevDayStock(DateTime dt);

        // 得到前一个工作日
        DateTime GetPreviousDay(DateTime dt);
        
        bool IsOperSuccess(DateTime dt, StockOper oper);

        void AddStock(DateTime dt, IStockData stock);

        IStockHistory GetPartStockHistory(DateTime startDate, DateTime endDate);
    }
}
