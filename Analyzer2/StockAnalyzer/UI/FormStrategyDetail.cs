using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.Strategy.Result;

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

            foreach (string strategyName in Results.AllStrategyNames)
            {
                IStockValues val = Results.GetResult(strategyName);

                ListViewItem listItem = new ListViewItem(strategyName);
                listItem.SubItems.Add(val.GetOperCount(OperType.Buy).ToString());
                listItem.SubItems.Add(val.GetOperCount(OperType.Sell).ToString());

                listViewStrategy.Items.Add(listItem);
            }
        }

        void AddGridTitle()
        {
            listViewStrategy.Columns.Add("Strategy Name");
            listViewStrategy.Columns.Add("Buy Signal");
            listViewStrategy.Columns.Add("Sell Signal");
        }
    }
}
