using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.Strategy;

namespace FinanceAnalyzer.UI
{
    public partial class FormStrategy : Form
    {
        public FormStrategy()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxStrategy.Items.Count; i++ )
            {
                if (checkedListBoxStrategy.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    string txt = checkedListBoxStrategy.Items[i].ToString();
                    _Factory.Remove(txt);
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormStrategy_Load(object sender, EventArgs e)
        {
            _Factory.Init();

            foreach (string val in _Factory.AllStrategyNames)
            {
                checkedListBoxStrategy.Items.Add(val, CheckState.Checked);
            }
        }

        public StrategyFactory Factory
        {
            get
            {
                return _Factory;
            }
        }

        StrategyFactory _Factory = new StrategyFactory();

        private void buttonClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxStrategy.Items.Count; i++)
            {
                checkedListBoxStrategy.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
    }
}
