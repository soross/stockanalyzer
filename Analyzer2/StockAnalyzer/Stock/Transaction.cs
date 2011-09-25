using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Stock
{
    // ����
    public sealed class Transaction
    {
        // ������
        private const double CommissionChargeRate = 0.0025;

        // ӡ��˰
        private const double StampDutyRate = 0.001;

        // �õ����Թ���Ĺ�Ʊ��Ŀ
        public static int GetCanBuyStockCount(double bankRoll, double unitPrice)
        {
            double count = bankRoll / ((unitPrice) * (1 + CommissionChargeRate + StampDutyRate));

            double remain = count - (count % 100);
            return (int)remain;
        }

        // ��Ʊ����һ�ε��ܳɱ�
        public static double GetTotalCharge(double tradingVolume)
        {
            return tradingVolume + GetDutyCharge(tradingVolume);
        }

        // ��Ʊ����һ�ε�˰�ѳɱ�
        public static double GetDutyCharge(double tradingVolume)
        {
            return (tradingVolume * TotalChargeRate());
        }

        // ��Ʊ����һ�εĳɱ�ռ���׶�ı��� 
        public static double TotalChargeRate()
        {
            return CommissionChargeRate + StampDutyRate;
        }

        private Transaction()
        {
        }
    }
}
