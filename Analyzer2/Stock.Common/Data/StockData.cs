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
            get { return Id_; }
            set { Id_ = value; }
        }        

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TradeDate
        {
            get { return TradeDate_; }
            set { TradeDate_ = value; }
        }

        public Int64 TradeDateBinary
        {
            get
            {
                return TradeDate_.ToBinary();
            }
        }

        public double StartPrice
        {
            get { return StartPrice_; }
            set { StartPrice_ = value; }
        }        

        public double EndPrice
        {
            get { return EndPrice_; }
            set { EndPrice_ = value; }
        }        

        public double MaxPrice
        {
            get { return MaxPrice_; }
            set { MaxPrice_ = value; }
        }        

        public double MinPrice
        {
            get { return MinPrice_; }
            set { MinPrice_ = value; }
        }

        // 成交手数
        public int VolumeHand
        {
            get { return VolumeHand_; }
            set { VolumeHand_ = value; }
        }

        // 成交额
        public double Amount
        {
            get { return Amount_; }
            set { Amount_ = value; }
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

        private int Id_;
        private DateTime TradeDate_;
        private double StartPrice_;
        private double EndPrice_;
        private double MaxPrice_;
        private double MinPrice_;
        private int VolumeHand_; // 成交手数
        private double Amount_; // 成交额
    }
}
