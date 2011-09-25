using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class CombinationCalc : BasicIndicatorCalc
    {
        public override string Name
        {
            get
            {
                string calcnames = "";
                foreach (IIndicatorCalc calc in _CalcArr)
                {
                    calcnames += calc.Name + ", ";
                }
                return "Combine " + calcnames;
            }
        }

        public void AddIndicator(IIndicatorCalc calc, IndicatorMixedType type)
        {
            _CalcArr.Add(calc);
            _IndicatorTypes.Add(calc.Name, type);
        }

        public override void Calc(IStockHistory hist)
        {
            foreach (IIndicatorCalc calc in _CalcArr)
            {
                calc.Calc(hist);
            }

            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                DateTime prev = DateFunc.GetPreviousWorkday(startDate);

                foreach (IIndicatorCalc calc in _CalcArr)
                {
                    OperType tp = calc.MatchSignal(startDate, prev);

                    if (tp == OperType.NoOper)
                    {
                        continue;
                    }

                    if (IsSignalValid(calc.Name, tp))
                    {
                        if (!_DateToOneOpers.ContainsKey(startDate))
                        {
                            _DateToOneOpers.Add(startDate, tp);
                        }
                        else
                        {
                            // At least two indicators fulfiled  
                            if (tp == _DateToOneOpers[startDate])
                            {
                                _DateToOpers.Add(startDate, tp);
                            }
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

        List<IIndicatorCalc> _CalcArr = new List<IIndicatorCalc>();
        Dictionary<string, IndicatorMixedType> _IndicatorTypes = new Dictionary<string, IndicatorMixedType>();

        Dictionary<DateTime, OperType> _DateToOneOpers = new Dictionary<DateTime, OperType>();
    }
}
