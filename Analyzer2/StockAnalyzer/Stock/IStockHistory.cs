using System;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
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

        int StockId
        {
            get;
        }

        // 得到第一天的股票属性
        IStockData GetFirstStock();

        // 得到某一天的股票属性
        IStockData GetStock(DateTime dt);

        IStockData GetPrevDayStock(DateTime dt);

        // 得到前一个工作日
        DateTime GetPreviousDay(DateTime dt);

        // 得到前N个工作日
        DateTime GetPreviousNDay(DateTime dt, int n);

        bool IsOperSuccess(DateTime dt, StockOper oper);

        void AddStock(DateTime dt, IStockData stock);
    }
}
