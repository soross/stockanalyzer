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

        IStrategyResults Results
        {
            set;
            private get;
        }

        private void FormStrategyDetail_Load(object sender, EventArgs e)
        {
            foreach (string strategyName in Results.AllStrategyNames)
            {
                IStockValues val = Results.GetResult(strategyName);


            }
        }
    }
}
