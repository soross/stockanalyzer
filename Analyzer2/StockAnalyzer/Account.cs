using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer
{
    /// <summary>
    /// 当前帐户
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// 现金
        /// </summary>
        double BankRoll
        {
            get;
        }
    }

    /// <summary>
    /// 账户：拥有现金，并且支持买入和卖出操作 
    /// </summary>
    public class Account : IAccount
    {
        /// <summary>
        /// 处理买入或者卖出操作
        /// </summary>
        /// <param name="oper">操作</param>
        /// <returns>操作是否成功</returns>
        public bool DoBusiness(StockOper operation)
        {
            if (operation == null)
            {
                return false;
            }
            
            if (operation.Type == OperType.Buy)
            {
                this.BuyStocks(operation);                
            }
            else if (operation.Type == OperType.Sell)
            {
                this.bankRoll += Holder.SellStock(operation.HandCount, operation.UnitPrice);
                this._SellTransactionCount++;
            }
            else
            {
            }

            return true;
        }

        /// <summary>
        /// 处理分红送配 
        /// </summary>
        /// <param name="date">日期，不一定是分红送配的日期</param>
        public void ProcessBonus(DateTime date)
        {
            if (this.Processor.IsExexDividendDate(date))
            {
                Bonus bonus = Processor.FindBonus(date);
                if (bonus != null)
                {
                    _Bonuses.Dividend = Holder.StockCount() * bonus.Dividend; // 总分红金额
                    _Bonuses.BonusCount = Convert.ToInt32(Holder.StockCount() * bonus.BonusCount); // 总送股数目
                    if (_Bonuses.Dividend != 0)
                    {
                    }
                }
                else
                {
                }
            }

            if (Processor.IsDividendDate(date))
            {
                bankRoll += _Bonuses.Dividend; // 现金加上分红
                _Bonuses.Dividend = 0;
            }

            if (Processor.IsBonusListOnDate(date))
            {
                _Holder.AddBonusStock(_Bonuses.BonusCount);
                _Bonuses.BonusCount = 0;
            }
        }

        // 买入股票
        private void BuyStocks(StockOper oper)
        {
            _BuyTransactionCount++;
            double charge = Transaction.GetTotalCharge(oper.UnitPrice * oper.HandCount);

            if (bankRoll >= charge)
            {
                bankRoll -= charge;
                Holder.BuyStock(oper.HandCount, oper.UnitPrice);

                LogMgr.Logger.LogInfo("Action: Buy Stock Count: {0}, Unit Price: {1}",
                    oper.HandCount,
                    oper.UnitPrice);
            }
            else
            {
                throw new InvalidOperationException("Bank Roll is smaller than total price!");
            }
        }

        /// <summary>
        /// 计算某个时间的总市值（股票市值+现金） 
        /// </summary>
        /// <param name="day">日期</param>
        /// <returns>总市值</returns>
        public double TotalValue(DateTime day)
        {
            return Holder.MarketValue(day) + bankRoll;
        }

        public double BankRoll
        {
            get
            {
                return bankRoll;
            }
            set
            {
                bankRoll = value;
            }
        }

        public IStockHolder Holder
        {
            get { return _Holder; }
            set { _Holder = value; }
        }

        public int BuyTransactionCount
        {
            get { return _BuyTransactionCount; }
        }

        public int SellTransactionCount
        {
            get { return _SellTransactionCount; }
        }

        public IBonusProcessor Processor
        {
            get;
            set;
        }

        BonusInfo _Bonuses = new BonusInfo();
        
        double bankRoll; // 现金

        int _BuyTransactionCount; // 交易次数
        int _SellTransactionCount;

        IStockHolder _Holder;
    }
}
