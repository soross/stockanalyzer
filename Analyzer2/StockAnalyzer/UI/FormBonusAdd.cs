using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.DB;
using System.Globalization;

namespace FinanceAnalyzer.UI
{
    public partial class FormBonusAdd : Form
    {
        public FormBonusAdd()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {                
                listBoxLog.Items.Add(DateTime.Now.ToShortTimeString() + ": data error, please check!");                
                return;
            }

            // 构造Bonus对象
            Bonus val = new Bonus();
            val.BonusYear = int.Parse(comboBoxYear.Text, CultureInfo.CurrentCulture);
            val.StockId = int.Parse(textBoxStockId.Text, CultureInfo.CurrentCulture);

            double bonusCount = double.Parse(textBoxBonuscount.Text, CultureInfo.CurrentCulture) / 10.0;
            double bonusAdded = double.Parse(textBoxBonusAdded.Text, CultureInfo.CurrentCulture) / 10.0;
            val.BonusCount = bonusCount + bonusAdded; // 送股和转增股相同处理
            val.Dividend = double.Parse(textBoxDividend.Text, CultureInfo.CurrentCulture) / 10.0;
            val.DividendDate = dateTimePickerDividend.Value;
            val.RegistOn = dateTimePickerRegistOn.Value;
            val.ExexDividend = dateTimePickerExex.Value;
            val.BonusListOn = dateTimePickerListOn.Value;

            // 转储到数据库
            BonusReader.InsertBonus(val);
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = int.Parse(comboBoxYear.Text, CultureInfo.CurrentCulture) + 1;

            dateTimePickerListOn.Value = new DateTime(year, 1, 1);
            dateTimePickerDividend.Value = new DateTime(year, 1, 1);
            dateTimePickerRegistOn.Value = new DateTime(year, 1, 1);
            dateTimePickerExex.Value = new DateTime(year, 1, 1);
        }

        private bool CheckInput()
        {
            if (String.IsNullOrEmpty(textBoxStockId.Text)
                || String.IsNullOrEmpty(textBoxBonuscount.Text)
                || String.IsNullOrEmpty(textBoxDividend.Text)
                || String.IsNullOrEmpty(textBoxBonusAdded.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
