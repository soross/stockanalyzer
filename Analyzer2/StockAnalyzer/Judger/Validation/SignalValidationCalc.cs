using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Judger.Validation
{
    class SignalValidationCalc
    {
        public SignalValidationCalc()
        {
            _SellPrice = -1;
        }

        public void AddSignal(DateTime dt, OperType tp)
        {
            if (tp == OperType.Buy)
            {
                if (_HasStock)
                {
                    // 不处理两次买入
                    return;
                }

                _BuyPrice = HoldValues.GetTotalValue(dt);
                _HasStock = true;

                // Valid last sell operation 
                if (_SellPrice > 0)
                {
                    _BuyScore += CalcScore();
                }
            }
            else if (tp == OperType.Sell)
            {
                if (!_HasStock)
                {
                    // 不处理两次卖出
                    return;
                }

                _HasStock = false;
                _SellPrice = HoldValues.GetTotalValue(dt);
                _SellScore += CalcScore();
            }
            else
            {
            }
        }

        private double CalcScore()
        {
            double percent = (_SellPrice - _BuyPrice) / _BuyPrice;
            percent -= Transaction.TotalChargeRate(); // 去除交易费用比例 

            double score = percent * 100;
            _TotalScore += score * MULTIPLYVAL;

            return score * MULTIPLYVAL;
        }

        public double TotalScore
        {
            get { return _TotalScore; }
        }

        public double BuyScore
        {
            get { return _BuyScore; }
        }

        public double SellScore
        {
            get { return _SellScore; }
        }

        public IStockValues HoldValues
        {
            private get;
            set;
        }

        const double MULTIPLYVAL = 1; // 放大倍数
        bool _HasStock;
        double _BuyPrice;
        double _SellPrice;
        
        double _TotalScore;
        double _BuyScore;        
        double _SellScore;        
    }
}
