using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Indicator
{
    public enum IndicatorMixedType
    {
        Buy,
        Sell,
        NoOper,
        BuyAndSell
    }

    class IMixedCalc : IIndicatorCalc
    {
        public string Name
        {
            get 
            { 
                string calcnames = "";
                foreach (IIndicatorCalc calc in _CalcArr)
                {
                    calcnames += calc.Name + ", ";
                }
                return "Mixed " + calcnames; 
            }
        }

        public void AddIndicator(IIndicatorCalc calc, IndicatorMixedType type)
        {
            _CalcArr.Add(calc);
            _IndicatorTypes.Add(calc.Name, type);
        }

        public void Calc(IStockHistory hist)
        {
            foreach (IIndicatorCalc calc in _CalcArr)
            {
                calc.Calc(hist);
            }

            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                DateTime prev = DateFunc.GetPrevWorkday(startDate);

                foreach (IIndicatorCalc calc in _CalcArr)
                {
                    OperType tp = calc.MatchSignal(startDate, prev);

                    if (IsSignalValid(calc.Name, tp))
                    {
                        if (!_DateToOpers.ContainsKey(startDate))
                        {
                            _DateToOpers.Add(startDate, tp);
                        }
                        else
                        {
                            // 同一天出现了多次指示，暂时只考虑第一次  
                        }
                    }
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        public bool IsSignalValid(string indicatorName, OperType operType)
        {
            if (!_IndicatorTypes.ContainsKey(indicatorName))
            {
                return false;
            }

            IndicatorMixedType tp = _IndicatorTypes[indicatorName];

            switch (tp)
            {
                case IndicatorMixedType.BuyAndSell:
                    return true;
                case IndicatorMixedType.Buy:
                    return (operType == OperType.Buy);
                case IndicatorMixedType.Sell:
                    return (operType == OperType.Sell);
                case IndicatorMixedType.NoOper:
                    return false;
                default:
                    return false;
            }
        }

        // 指令为
        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (!_DateToOpers.ContainsKey(dt))
            {
                return OperType.NoOper;
            }

            return _DateToOpers[dt];
        }

        List<IIndicatorCalc> _CalcArr = new List<IIndicatorCalc>();
        Dictionary<string, IndicatorMixedType> _IndicatorTypes = new Dictionary<string, IndicatorMixedType>();
        Dictionary<DateTime, OperType> _DateToOpers = new Dictionary<DateTime, OperType>();
    }
}
