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
                this.bankRoll += Holder.SellStock(operation.HandCount, operation.UnitPrice);
                this._SellTransactionCount++;
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
                bankRoll += _Bonuses.Dividend; // �ֽ���Ϸֺ�
                _Bonuses.Dividend = 0;
            }

            if (Processor.IsBonusListOnDate(date))
            {
                _Holder.AddBonusStock(_Bonuses.BonusCount);
                _Bonuses.BonusCount = 0;
            }
        }

        // �����Ʊ
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
        /// ����ĳ��ʱ�������ֵ����Ʊ��ֵ+�ֽ� 
        /// </summary>
        /// <param name="day">����</param>
        /// <returns>����ֵ</returns>
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
        
        double bankRoll; // �ֽ�

        int _BuyTransactionCount; // ���״���
        int _SellTransactionCount;

        IStockHolder _Holder;
    }
}
