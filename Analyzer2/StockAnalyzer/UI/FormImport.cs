using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.UI
{
    public partial class FormImport : Form
    {
        public FormImport()
        {
            InitializeComponent();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPath.Text))
            {
                return;
            }

            if (radioButtonYahoo.Checked)
            {
                _DbImporter.ImportYahoo(textBoxPath.Text);
            }
            else
            {
                _DbImporter.Import(textBoxPath.Text);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            _DbImporter.SetStockSaver(new StockDBSaver());
        }

        StockImporter2 _DbImporter = new StockImporter2();

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "xml";
            dlg.Filter = "Stock files (*.xml)|*.xml|All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = dlg.FileName;
            }
        }
    }
}
