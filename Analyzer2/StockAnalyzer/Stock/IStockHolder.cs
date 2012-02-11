using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// manage buyed stocks and bonuses
    /// </summary>
    public interface IStockHolder
    {
        bool HasStock();

        void BuyStock(int count, double unitPrice);

        void AddBonusStock(int count);

        int StockCount();

        // ��Ʊ��ֵ
        double MarketValue(DateTime day);

        // ����ֵΪ��õĽ��
        double SellStock(int count, double unitPrice);

        // ��λ�ɱ�
        double UnitPrice
        {
            get;
        }

        IStockHistory History
        {
            get;
            set;
        }
    }
}
