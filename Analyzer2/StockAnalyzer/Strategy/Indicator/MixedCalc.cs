using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    public enum IndicatorMixedType
    {
        Buy,
        Sell,
        NoOper,
        BuyAndSell
    }

    class MixedCalc : BasicIndicatorCalc
    {
        public override string Name
        {
            get 
            { 
                string calcnames = "";
                foreach (IIndicatorCalc calc in IndicatorsArr_)
                {
                    calcnames += calc.Name + ", ";
                }
                return "Mixed " + calcnames; 
            }
        }

        public void AddIndicator(IIndicatorCalc calc, IndicatorMixedType type)
        {
            IndicatorsArr_.Add(calc);
            IndicatorTypes_.Add(calc.Name, type);
        }

        public override void Calc(IStockHistory hist)
        {
            foreach (IIndicatorCalc calc in IndicatorsArr_)
            {
                calc.Calc(hist);
            }

            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                DateTime prev = DateFunc.GetPreviousWorkday(startDate);

                foreach (IIndicatorCalc calc in IndicatorsArr_)
                {
                    OperType tp = calc.MatchSignal(startDate, prev);

                    if (tp == OperType.NoOper)
                    {
                        continue;
                    }

                    if (IsSignalValid(calc.Name, tp))
                    {
                        if (!DateToOpers_.ContainsKey(startDate))
                        {
                            DateToOpers_.Add(startDate, tp);
                        }
                        else
                        {
                            // 同一天出现了多次指示，暂时只考虑第一次  
                            LogMgr.Logger.LogInfo("Calc Name: {0}, Indicator: {1}, ignored in Date: {2}!", 
                                calc.Name, tp.ToString(), startDate.ToShortDateString());
                        }
                    }
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        public bool IsSignalValid(string indicatorName, OperType operType)
        {
            if (!IndicatorTypes_.ContainsKey(indicatorName))
            {
                return false;
            }

            IndicatorMixedType tp = IndicatorTypes_[indicatorName];

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

        List<IIndicatorCalc> IndicatorsArr_ = new List<IIndicatorCalc>();
        Dictionary<string, IndicatorMixedType> IndicatorTypes_ = new Dictionary<string, IndicatorMixedType>();
    }
}
