using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// Calculate results using specified stock and strategies
    /// </summary>
    public class FinanceRunner
    {
        private const double INITCASH = 100000; // 初始化的账户金额 

        public void Go(IStockHistory history, IStrategyFactory strategies)
        {
            History_ = history;

            foreach (IFinanceStrategy strategy in strategies.AllStrategies)
            {
                ExecTask(strategy);
            }
        }

        // 根据选定的策略开始计算
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
            holder.History = History_;
            acc.Holder = holder;
            strategy.Holder = holder;
            HolderManager.Instance().Holder = holder;
            
            acc.Processor = CurrentBonusProcessor;

            DateTime startDate = History_.MinDate;
            DateTime endDate = History_.MaxDate;

            LogMgr.Logger.LogInfo("Min Date = {0}, Max Date = {1}", startDate, endDate);
            Debug.Assert(endDate > startDate);

            while (startDate < endDate)
            {
                double totalvalue = acc.TotalValue(startDate);
                values.SetTotalValue(startDate, totalvalue);
                
                //acc.ProcessBonus(startDate);

                ICollection<StockOper> opers = strategy.GetOper(startDate, acc);

                if (opers != null)
                {
                    foreach (StockOper oper in opers)
                    {
                        if (History_.IsOperSuccess(startDate, oper))
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
        IStockHistory History_;

        public IBonusProcessor CurrentBonusProcessor
        {
            get;
            set;
        }
    }
}
