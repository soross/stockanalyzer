using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer
{
    // 保存所有交易日每一天的市值（股票资产加现金） 
    public interface IStockValues
    {
        // 得到某天的市值
        double GetTotalValue(DateTime dt);

        // 设置某天的市值
        void SetTotalValue(DateTime dt, double val);

        // 得到所有的日期
        ICollection<DateTime> GetAllDate();

        // 设置当日操作类型
        void SetOperationSignal(DateTime dt, OperType val);

        // 获取当日操作类型
        OperType GetOperationSignal(DateTime dt);
    }
}
