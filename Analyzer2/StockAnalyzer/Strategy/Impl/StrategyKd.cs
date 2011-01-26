using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.KDJ;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Impl
{
    // KDJָ��ĵ�λK�ϴ�D�ߺ͸�λK�´�D���ж�
    public class StrategyKD : IFinanceStrategy
    {
        public override string Name
        {
            get
            {
                return "KD " + _KdMinMargin.ToString() + ", " + _KdMaxMargin.ToString();
            }
        }

        public StrategyKD(double kdmin, double kdmax)
        {
            _KdMinMargin = kdmin;
            _KdMaxMargin = kdmax;
        }

        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curProp = stockHistory.GetStock(day);
            double k = _Storage.GetK(day);
            double d = _Storage.GetD(day);

            DateTime yesterday = stockHistory.GetPreviousDay(day);
            double prevK = _Storage.GetK(yesterday);
            double prevD = _Storage.GetD(yesterday);

            ICollection<StockOper> opers = new List<StockOper>();

            // ��λK�ϴ�D��
            if (KUpCrossD(k, d, prevK, prevD))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.EndPrice);
                if (stockCount <= 0)
                {
                    return null;
                }
                StockOper oper = new StockOper(curProp.EndPrice, stockCount, OperType.Buy);
                opers.Add(oper);
                return opers; // ����
            }

            // ��λK�´�D��
            if (KDownCrossD(k, d, prevK, prevD))
            {
                StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                opers.Add(oper);
                return opers;
            }

            return null;
        }

        private bool KUpCrossD(double k, double d, double prevK, double prevD)
        {
            return (k < _KdMinMargin) && (d < _KdMinMargin) && (k > d) && (prevK < prevD);
        }

        private bool KDownCrossD(double k, double d, double prevK, double prevD)
        {
            return (k > _KdMaxMargin) && (d > _KdMaxMargin) && (k < d) && (prevK > prevD);
        }

        protected override void HolderInit()
        {
            _KdjCalc.Calc(stockHistory);

            _Storage = _KdjCalc.GetStorage();
        }

        double _KdMinMargin; // ��λ����
        double _KdMaxMargin; // ��λ����
        //const double KDMINMARGIN = 25; // ��λ����
        //const double KDMAXMARGIN = 75; // ��λ����

        IKdjCalculator _KdjCalc = new KdjCalculator();
        IKdjStorage _Storage;
    }
}
