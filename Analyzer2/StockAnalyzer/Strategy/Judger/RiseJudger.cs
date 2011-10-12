using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Judger
{
    public class RiseJudger : IStockJudger
    {
        // ��ʱ�������������죬�ж��Ƿ���������
        // Day1��ǰ��Day2��һЩ. 
        public bool FulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // ����������������
            return (StockJudger.IsRise(day3, day2) && StockJudger.IsRise(day2, day1));
        }

        // ��ʱ�������������죬�ж��Ƿ������෴������
        // Day1��ǰ��Day2��һЩ. 
        public bool ReverseFulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // ���������µ�����
            return (!StockJudger.IsRise(day3, day2) && !StockJudger.IsRise(day2, day1));
        }

        public string Name
        {
            get
            {
                return "Compare prev";
            }
        }
    }
}
