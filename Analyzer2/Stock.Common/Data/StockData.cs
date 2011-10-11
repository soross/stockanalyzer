using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Stock.Common.Data
{
    // 保存股票交易某只股票某一天的信息
    public class StockData : IStockData
    {
        public ObjectId Id { get; set; }

        // 股票代码
        public int StockId
        {
            get { return _Id; }
            set { _Id = value; }
        }        

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
