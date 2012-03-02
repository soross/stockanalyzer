using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Log;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// ��ǰ�ʻ�
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// �ֽ�
        /// </summary>
        double BankRoll
        {
            get;
        }
    }

    /// <summary>
    /// �˻���ӵ���ֽ𣬲���֧��������������� 
    /// </summary>
    public class Account : IAccount
    {
        /// <summary>
        /// �������������������
        /// </summary>
        /// <param name="oper">����</param>
        /// <returns>�����Ƿ�ɹ�</returns>
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
                this.bankRoll_ += Holder.SellStock(operation.HandCount, operation.UnitPrice);
                this.SellTransactionCount_++;
            }
            else
            {
            }

            return true;
        }

        /// <summary>
        /// ����ֺ����� 
        /// </summary>
        /// <param name="date">���ڣ���һ���Ƿֺ����������</param>
        public void ProcessBonus(DateTime date)
        {
            if (this.Processor.IsExexDividendDate(date))
            {
                Bonus bonus = Processor.FindBonus(date);
                if (bonus != null)
                {
                    _Bonuses.Dividend = Holder.StockCount() * bonus.Dividend; // �ֺܷ���
                    _Bonuses.BonusCount = Convert.ToInt32(Holder.StockCount() * bonus.BonusCount); // ���͹���Ŀ
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
                bankRoll_ += _Bonuses.Dividend; // �ֽ���Ϸֺ�
                _Bonuses.Dividend = 0;
            }

            if (Processor.IsBonusListOnDate(date))
            {
                StockHolder_.AddBonusStock(_Bonuses.BonusCount);
                _Bonuses.BonusCount = 0;
            }
        }

        // �����Ʊ
        private void BuyStocks(StockOper oper)
        {
            BuyTransactionCount_++;
            double charge = Transaction.GetTotalCharge(oper.UnitPrice * oper.HandCount);

            if (bankRoll_ >= charge)
            {
                bankRoll_ -= charge;
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
        /// ����ĳ��ʱ�������ֵ����Ʊ��ֵ+�ֽ� 
        /// </summary>
        /// <param name="day">����</param>
        /// <returns>����ֵ</returns>
        public double TotalValue(DateTime day)
        {
            return Holder.MarketValue(day) + bankRoll_;
        }

        /// <summary>
        /// �˻��е��ֽ�
        /// </summary>
        public double BankRoll
        {
            get
            {
                return bankRoll_;
            }
            set
            {
                bankRoll_ = value;
            }
        }

        /// <summary>
        /// buyed stocks and bonuses
        /// </summary>
        public IStockHolder Holder
        {
            get { return StockHolder_; }
            set { StockHolder_ = value; }
        }

        /// <summary>
        /// ���뽻�״���
        /// </summary>
        public int BuyTransactionCount
        {
            get { return BuyTransactionCount_; }
        }

        /// <summary>
        /// �������״���
        /// </summary>
        public int SellTransactionCount
        {
            get { return SellTransactionCount_; }
        }

        public IBonusProcessor Processor
        {
            get;
            set;
        }

        BonusInfo _Bonuses = new BonusInfo();
        
        double bankRoll_; // �ֽ�

        int BuyTransactionCount_; // ���״���
        int SellTransactionCount_;

        IStockHolder StockHolder_;
    }
}
