using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.DB;
using System.Globalization;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Stock
{
    public class FinanceRunner
    {
        private const double INITCASH = 100000; // ��ʼ�����˻���� 

        public void Go(IStockHistory history, IStrategyFactory strategies)
        {
            _History = history;

            foreach (IFinanceStrategy strategy in strategies.AllStrategies)
            {
                ExecTask(strategy);
            }
        }

        // ����ѡ���Ĳ��Կ�ʼ����
        private void ExecTask(IFinanceStrategy strategy)
        {
            IStockValues values = new StockValues();

            Start(values, strategy);

            _results.AddResult(strategy.Name, values);
        }
        
        private void Start(IStockValues values, IFinanceStrategy strategy)
        {
            LogMgr.Logger.LogInfo("==>Start With Strategy: " + strategy.Name + "...");
            Account acc = new Account();
            acc.BankRoll = INITCASH;

            IStockHolder holder = new StockHolder();            
            holder.History = _History;
            acc.Holder = holder;
            strategy.Holder = holder;
            HolderManager.Instance().Holder = holder;
            
            acc.Processor = CurrentBonusProcessor;

            DateTime startDate = _History.MinDate;
            DateTime endDate = _History.MaxDate;

            LogMgr.Logger.LogInfo("Min Date = {0}, Max Date = {1}", startDate, endDate);
            Debug.Assert(endDate > startDate);

            while (startDate < endDate)
            {
                double totalvalue = acc.TotalValue(startDate);
                values.SetTotalValue(startDate, totalvalue);
                
                acc.ProcessBonus(startDate);

                ICollection<StockOper> opers = strategy.GetOper(startDate, acc);

                if (opers != null)
                {
                    foreach (StockOper oper in opers)
                    {
                        if (_History.IsOperSuccess(startDate, oper))
                        {
                            acc.DoBusiness(oper);
                            values.SetOperationSignal(startDate, oper.Type);
                        }                        
                    }
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }

            LogMgr.Logger.LogInfo("Strategy " + strategy.Name + " End Value: "
                + acc.TotalValue(startDate).ToString(CultureInfo.CurrentCulture));
            LogMgr.Logger.LogInfo("Strategy " + strategy.Name 
                + ": Buys: " + acc.BuyTransactionCount
                + ", Sells: " + acc.SellTransactionCount);
            LogMgr.Logger.LogInfo("<== End With Strategy: " + strategy.Name);
        }

        public StrategyResults Results
        {
            get { return _results; }
        }

        StrategyResults _results = new StrategyResults();
        IStockHistory _History;

        public IBonusProcessor CurrentBonusProcessor
        {
            get;
            set;
        }
    }
}
