using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Common.Data
{
    /// <summary>
    /// 用于保存一只股票每一天的信息
    /// </summary>
    public interface IStockData
    {
        int StockId
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

        bool AllPriceSame
        {
            get;
        }
    }
}
