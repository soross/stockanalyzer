using System;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer
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
        StockData GetFirstStock();

        // 得到某一天的股票属性
        StockData GetStock(DateTime dt);

        StockData GetPrevDayStock(DateTime dt);

        // 得到前一个工作日
        DateTime GetPrevDay(DateTime dt);

        bool IsOperSuccess(DateTime dt, StockOper oper);
    }
}
