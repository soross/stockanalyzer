using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.DB
{
    public interface IStockData
    {
        int Id
        {
            get;
        }

        DateTime TradeDate
        {
            get;
        }

        double StartPrice
        {
            get;
        }

        double EndPrice
        {
            get;
        }

        double MaxPrice
        {
            get;
        }

        double MinPrice
        {
            get;
        }

        int VolumeHand
        {
            get;
        }

        double Amount
        {
            get;
        }
    }
}
