using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FinanceAnalyzer
{
    public enum OperType
    {
        Buy,
        Sell,
        NoOper
    }

    public class StockOper
    {
        public StockOper(double price, int count, OperType type)
        {
            Stock stock = new Stock();
            stock.UnitPrice = price;
            stock.Count = count;

            TargetStock = stock;
            operType = type;
        }

        public OperType Type
        {
            get
            {
                return operType;
            }
        }

        public double UnitPrice
        {
            get
            {
                return TargetStock.UnitPrice;
            }            
        }

        public int HandCount
        {
            get
            {
                return TargetStock.Count;
            }
        }

        private Stock TargetStock;
        private OperType operType;
    }    
}
