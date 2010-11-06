using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.Judger;

namespace FinanceAnalyzer.UI
{
    public partial class FormScores : Form
    {
        public FormScores()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public ICollection<IStrategyScores> ScoresArr
        {
            private get;
            set;
        }

        private void FormScores_Load(object sender, EventArgs e)
        {
            InitCharts();
        }

        private void InitCharts()
        {
            foreach (IStrategyScores scores in ScoresArr)
            {
                chart1.ChartAreas.Add(scores.Name);
                chart1.Series.Add(scores.Name);
                chart1.Series[scores.Name].ChartArea = scores.Name;

                foreach (string strategyname in scores.GetTopStrategyNames(5))
                {
                    chart1.Series[scores.Name].Points.AddXY(strategyname, scores.GetScore(strategyname));
                }
            }
        }
    }
}
