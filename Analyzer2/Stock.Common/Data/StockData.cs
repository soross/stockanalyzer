using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Stock.Common.Data
{
    /// <summary>
    /// 保存股票交易某只股票某一天的信息
    /// </summary>
    public class StockData : IStockData
    {
        /// <summary>
        /// For MongoDB
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// 股票代码。如上证 600019
        /// </summary>
        public int StockId
        {
            get { return _Id; }
            set { _Id = value; }
        }        

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TradeDate
        {
            get { return _TradeDate; }
            set { _TradeDate = value; }
        }        

        public double StartPrice
        {
            get { return _StartPrice; }
            set { _StartPrice = value; }
        }        

        public double EndPrice
        {
            get { return _EndPrice; }
            set { _EndPrice = value; }
        }        

        public double MaxPrice
        {
            get { return _MaxPrice; }
            set { _MaxPrice = value; }
        }        

        public double MinPrice
        {
            get { return _MinPrice; }
            set { _MinPrice = value; }
        }

        // 成交手数
        public int VolumeHand
        {
            get { return _VolumeHand; }
            set { _VolumeHand = value; }
        }

        // 成交额
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public bool AllPriceSame
        {
            get
            {
                return PriceSame(StartPrice, MaxPrice) 
                    && PriceSame(MaxPrice, MinPrice) 
                    && PriceSame(MinPrice, EndPrice);
            }
        }

        static bool PriceSame(double price1, double price2)
        {
            return Math.Abs(price1 - price2) < 0.01;
        }

        private int _Id;
        private DateTime _TradeDate;
        private double _StartPrice;
        private double _EndPrice;
        private double _MaxPrice;
        private double _MinPrice;
        private int _VolumeHand; // 成交手数
        private double _Amount; // 成交额
    }
}
