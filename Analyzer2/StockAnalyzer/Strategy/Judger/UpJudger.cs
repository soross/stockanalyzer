using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Judger
{
    public class UpJudger : IStockJudger
    {
        // ��ʱ�������������죬�ж��Ƿ���������
        // Day1��ǰ��Day2��һЩ. 
        public bool FulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // ����������������
            return StockJudger.IsUp(day1) && StockJudger.IsUp(day2) && StockJudger.IsUp(day3);
        }

        // ��ʱ�������������죬�ж��Ƿ������෴������
        // Day1��ǰ��Day2��һЩ. 
        public bool ReverseFulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // ���������µ�����
            return !StockJudger.IsUp(day1) && !StockJudger.IsUp(day2) && !StockJudger.IsUp(day3);
        }

        public string Name
        {
            get
            {
                return "Up";
            }
        }
    }
}
