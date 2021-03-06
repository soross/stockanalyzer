﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using FinanceAnalyzer.Judger;
using FinanceAnalyzer.Judger.Validation;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Strategy.Result;
using Stock.Common.Data;

namespace FinanceAnalyzer.UI
{
    public partial class FormStrategyDetail : Form
    {
        public FormStrategyDetail()
        {
            InitializeComponent();
        }

        public IStrategyResults Results
        {
            set;
            get;
        }

        private void FormStrategyDetail_Load(object sender, EventArgs e)
        {
            AddGridTitle();

            ValidationJudger judger = new ValidationJudger();
            judger.Judge(Results);

            StrategyJudger strategyJudger = new StrategyJudger();
            strategyJudger.Judge(Results);

            InitScoresMapping(judger, strategyJudger);

            foreach (string strategyName in Results.AllStrategyNames)
            {
                IStockValues val = Results.GetResult(strategyName);

                ListViewItem listItem = new ListViewItem(strategyName);
                listItem.SubItems.Add(val.GetOperCount(OperType.Buy).ToString());
                listItem.SubItems.Add(val.GetOperCount(OperType.Sell).ToString());
                listItem.SubItems.Add(FindScore("Buy Signal", strategyName));
                listItem.SubItems.Add(FindScore("Sell Signal", strategyName));
                listItem.SubItems.Add(FindScore("Buy and Sell Signal", strategyName));
                listItem.SubItems.Add(FindScore("Daily Prices Sigma", strategyName));

                listViewStrategy.Items.Add(listItem);
            }
        }

        void AddGridTitle()
        {
            listViewStrategy.Columns.Add("Strategy Name");
            listViewStrategy.Columns.Add("Buy Signal");
            listViewStrategy.Columns.Add("Sell Signal");

            listViewStrategy.Columns.Add("Buy Score");
            listViewStrategy.Columns.Add("Sell Score");
            listViewStrategy.Columns.Add("Total Score");

            listViewStrategy.Columns.Add("Daily Prices Score");

            listViewStrategy.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        void InitScoresMapping(IStrategyJudger judger, IStrategyJudger judger2)
        {
            ScoresMapping_ = new Dictionary<string, IStrategyScores>();

            foreach (var item in judger.ScoresArr)
            {
                ScoresMapping_.Add(item.Name, item);
            }

            foreach (var item in judger2.ScoresArr)
            {
                ScoresMapping_.Add(item.Name, item);
            }
        }

        string FindScore(string scoresType, string strategyName)
        {
            if (!ScoresMapping_.ContainsKey(scoresType))
            {
                return "-";
            }

            IStrategyScores scores = ScoresMapping_[scoresType];
            if (scores == null)
            {
                return "-";
            }

            return scores.GetScore(strategyName).ToString("F02", CultureInfo.CurrentCulture);
        }

        Dictionary<string, IStrategyScores> ScoresMapping_;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
