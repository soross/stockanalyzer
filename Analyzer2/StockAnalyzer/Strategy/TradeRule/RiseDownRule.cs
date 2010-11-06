using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Strategy.TradeRule
{
    enum StockDirection
    {
        Rise,
        Down
    }

    class RiseDownRule
    {
        public RiseDownRule(double percent)
        {
            _CurMaxPrise = -1;
            _CurMinPrise = 10000;
            _Direction = StockDirection.Rise;
            _RiseDownPercent = percent;
        } 

        public void Init(double maxPrice, double minPrice)
        {
            _CurMaxPrise = maxPrice;
            _CurMinPrise = minPrice;
        }

        public OperType Execute(double curPrice, double prevPrice)
        {
            if (!JudgeSameDirection(curPrice, prevPrice))
            {
                _RevertSignal = true;
            }

            if (_RevertSignal)
            {
                if (curPrice < (_CurMaxPrise * (1 - _RiseDownPercent)))
                {
                    // 卖出信号
                    _CurMaxPrise = _CurMinPrise = curPrice;
                    return OperType.Sell;
                }

                if (curPrice > (_CurMinPrise * (1 + _RiseDownPercent)))
                {
                    // 买入信号
                    _CurMaxPrise = _CurMinPrise = curPrice;
                    return OperType.Buy;
                }
            }

            if (curPrice > _CurMaxPrise)
            {
                _CurMaxPrise = curPrice;
            }

            if (curPrice < _CurMinPrise)
            {
                _CurMinPrise = curPrice;
            }

            return OperType.NoOper;
        }

        public double RiseDownPercent
        {
            get
            {
                return _RiseDownPercent;
            }
        }

        private bool JudgeSameDirection(double curPrice, double prevPrice)
        {
            if ((curPrice > prevPrice) && (_Direction == StockDirection.Down))
            {
                _Direction = StockDirection.Rise;
                return false;
            }

            if ((curPrice < prevPrice) && (_Direction == StockDirection.Rise))
            {
                _Direction = StockDirection.Down;
                return false;
            }

            return true;
        }

        private double _CurMaxPrise;
        private double _CurMinPrise;

        private bool _RevertSignal; // 翻转信号 
        private StockDirection _Direction; // 记录当日收盘对比昨日升或降 

        private double _RiseDownPercent; // 比最高点下降的比例，比最低点上升的比例 

        private const int HOLDPERIOD = 5; // 持有期，在此期间忽略买入和卖出信号  
    }
}
