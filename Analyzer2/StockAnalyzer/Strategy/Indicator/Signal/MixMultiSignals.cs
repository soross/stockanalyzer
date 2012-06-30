using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    public enum IndicatorMixedType
    {
        Buy,
        Sell,
        NoOper,
        BuyAndSell
    }

    class MixMultiSignals : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            bool totalResult = true;
            foreach (ISignalCalculator calc in IndicatorsArr_)
            {
                totalResult = totalResult && calc.AddStock(sd);
            }

            return totalResult;
        }

        public OperType GetSignal()
        {
            foreach (ISignalCalculator calc in IndicatorsArr_)
            {
                OperType ot = calc.GetSignal();

                // TODO: only the first operation signal is considered 
                if ((ot == OperType.Buy) && IsBuyValid(calc.GetName()))
                {
                    return OperType.Buy;
                }
                else if ((ot == OperType.Sell) && IsSellValid(calc.GetName()))
                {
                    return OperType.Sell;
                }
                else
                {
                    return OperType.NoOper;
                }
            }

            return OperType.NoOper;
        }

        public string GetName()
        {
            string mixedName = "";
            foreach (ISignalCalculator calc in IndicatorsArr_)
            {
                mixedName += calc.GetName() + " ";
            }

            return mixedName;
        }

        #endregion

        public void AddIndicator(ISignalCalculator calc, IndicatorMixedType type)
        {
            IndicatorsArr_.Add(calc);
            IndicatorTypes_.Add(calc.GetName(), type);
        }

        private bool IsBuyValid(string signalName)
        {
            IndicatorMixedType mt = IndicatorTypes_[signalName];

            return (mt == IndicatorMixedType.Buy) || (mt == IndicatorMixedType.BuyAndSell);
        }

        private bool IsSellValid(string signalName)
        {
            IndicatorMixedType mt = IndicatorTypes_[signalName];

            return (mt == IndicatorMixedType.Sell) || (mt == IndicatorMixedType.BuyAndSell);
        }

        List<ISignalCalculator> IndicatorsArr_ = new List<ISignalCalculator>();
        Dictionary<string, IndicatorMixedType> IndicatorTypes_ = new Dictionary<string, IndicatorMixedType>();
    }
}
