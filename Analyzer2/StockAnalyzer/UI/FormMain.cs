using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.UI;
using IBatisNet.DataMapper;
using FinanceAnalyzer.DataAcquisition;
using FinanceAnalyzer.Strategy.Factory;
using Stock.Common.Data;

namespace FinanceAnalyzer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            UIFileLog logger = new UIFileLog();

            logger.LogListBox = listBoxLog;

            UITraceLog tracelog = new UITraceLog();
            tracelog.LogListBox = listBoxLog;

            LogMgr.Logger = logger;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Browse for file
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "txt";
            dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Show dialog
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StreamWriter writer = new StreamWriter(dlg.FileName);
            foreach (object obj in listBoxLog.Items)
            {
                writer.WriteLine(obj.ToString());
            }

            writer.Close();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogMgr.Close();
            _log.Info("Analyzer Exit.");
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            ShowCompareResults();
        }

        private void ShowCompareResults()
        {
            if (_results != null)
            {
                FormResultChart frm = new FormResultChart();

                _History.MaxDate = dateTimePickerEnd.Value;
                _History.MinDate = dateTimePickerStart.Value;
                frm.SetStockDrawer(_History.GetStockDrawer());

                frm.SetStrategyResults(_results);

                frm.ShowDialog();
            }
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            SetUserDefinedDate();

            _log.Info("==>Calculate start. Start Date = " + _History.MinDate.ToLongDateString() 
                + ", End Date = " + _History.MaxDate.ToLongDateString());

            FormStrategy frm = new FormStrategy();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                _log.Info("<==Calculate end. Not select strategy. ");
                return;
            }

            FinanceRunner runner = new FinanceRunner();            
            runner.CurrentBonusProcessor = _BonusProcessor;

            runner.Go(_History, frm.Factory);

            _results = runner.Results;
            _log.Info("<==Calculate complete. ");

            ShowCompareResults();
        }

        StrategyResults _results;

        private void buttonResetStart_Click(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = DateTime.Parse(labelMinDate.Text, CultureInfo.CurrentCulture);
            _History.MinDate = dateTimePickerStart.Value;
        }

        private void buttonResetEnd_Click(object sender, EventArgs e)
        {
            dateTimePickerEnd.Value = DateTime.Parse(labelMaxDate.Text, CultureInfo.CurrentCulture);
            _History.MaxDate = dateTimePickerEnd.Value;
        }

        private void ShowStockInfo()
        {
            labelMaxDate.Text = _History.MaxDate.ToShortDateString();
            labelMinDate.Text = _History.MinDate.ToShortDateString();

            dateTimePickerStart.MaxDate = _History.MaxDate;
            dateTimePickerStart.MinDate = _History.MinDate;
            dateTimePickerStart.Value = _History.MinDate;

            dateTimePickerEnd.MaxDate = _History.MaxDate;
            dateTimePickerEnd.MinDate = _History.MinDate;
            dateTimePickerEnd.Value = _History.MaxDate;
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void FormMain_Load(object sender, EventArgs e)
        {            
            // Load from DB
            IList<int> allStockId = _DbReader.LoadId();
            _log.Info("Form loaded. Stock Count: " + allStockId.Count);

            foreach (int id in allStockId)
            {
                comboBoxStockId.Items.Add(id);
            }

            _BonusProcessor.Load(_History.StockId, new BonusReader());

            //ShDailyDownloader downloader = new ShDailyDownloader();
            //downloader.DownloadData(null, 600036);
        }

        private void comboBoxStockId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(comboBoxStockId.Text, CultureInfo.CurrentCulture);
            IList<StockData> stocks = _DbReader.Load(id);

            _History.InitAllStocks(id, stocks);

            // 刷新界面显示
            ShowStockInfo();
        }

        StockHistory _History = new StockHistory();

        private void buttonAutoComp_Click(object sender, EventArgs e)
        {
            RunStrategy(new StrategyFactory());
        }

        private void buttonCalcAdjust_Click(object sender, EventArgs e)
        {
            RunStrategy(new SpikeAdjustFactory());
        }

        private void RunStrategy(StrategyFactory factory)
        {
            SetUserDefinedDate();
            _log.Info("==>AutoCompare start. Start Date = " + _History.MinDate.ToLongDateString()
                + ", End Date = " + _History.MaxDate.ToLongDateString());

            if (_History.MinDate >= _History.MaxDate)
            {
                _log.Error("Stock histroy not correct!");
                return;
            }

            factory.Init();

            ScoresCalculator calc = new ScoresCalculator();
            calc.Calc(_History, factory, _BonusProcessor);
            calc.ShowResult();
        }

        private void SetUserDefinedDate()
        {
            _History.MaxDate = dateTimePickerEnd.Value;
            _History.MinDate = dateTimePickerStart.Value;
        }        

        StockDBReader _DbReader = new StockDBReader(Mapper.Instance());
        
        BonusProcessor _BonusProcessor = new BonusProcessor();

        private void importYahooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormImport frm = new FormImport();
            frm.ShowDialog();
        }

        private void addBounusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBonusAdd frm = new FormBonusAdd();
            frm.Show();
        }

        // Display Japanese type chart for a stock
        private void stockChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void checkConsistencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 检查数据库中的数据
            _History.Check(LogMgr.Logger);
        }

        private void buttonChart_Click(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void ShowChart()
        {
            FormMSChart chart = new FormMSChart();

            _History.MaxDate = dateTimePickerEnd.Value;
            _History.MinDate = dateTimePickerStart.Value;
            chart.SetStockDrawer(_History.GetStockDrawer());

            chart.ShowDialog();
        }
    }
}