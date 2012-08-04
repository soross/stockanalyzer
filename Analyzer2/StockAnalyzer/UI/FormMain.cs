using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Factory;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.UI;
using Stock.Common.Data;
using FinanceAnalyzer.Statistics.Weekly;

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
            log_.Info("Analyzer Exit.");
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

                History_.MaxDate = dateTimePickerEnd.Value;
                History_.MinDate = dateTimePickerStart.Value;
                frm.SetStockDrawer(History_.GetStockDrawer());

                frm.SetStrategyResults(_results);

                frm.ShowDialog();
            }
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            SetUserDefinedDate();

            log_.Info("==>Calculate start. Start Date = " + History_.MinDate.ToLongDateString()
                + ", End Date = " + History_.MaxDate.ToLongDateString());

            FormStrategy frm = new FormStrategy();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                log_.Info("<==Calculate end. Not select strategy. ");
                return;
            }

            FinanceRunner runner = new FinanceRunner();
            runner.CurrentBonusProcessor = BonusProcessor_;

            runner.Go(History_, frm.Factory);

            _results = runner.Results;
            log_.Info("<==Calculate complete. ");

            ShowCompareResults();
        }

        StrategyResults _results;

        private void buttonResetStart_Click(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = DateTime.Parse(labelMinDate.Text, CultureInfo.CurrentCulture);
            History_.MinDate = dateTimePickerStart.Value;
        }

        private void buttonResetEnd_Click(object sender, EventArgs e)
        {
            dateTimePickerEnd.Value = DateTime.Parse(labelMaxDate.Text, CultureInfo.CurrentCulture);
            History_.MaxDate = dateTimePickerEnd.Value;
        }

        private void ShowStockInfo()
        {
            labelMaxDate.Text = History_.MaxDate.ToShortDateString();
            labelMinDate.Text = History_.MinDate.ToShortDateString();

            dateTimePickerStart.MaxDate = History_.MaxDate;
            dateTimePickerStart.MinDate = History_.MinDate;
            dateTimePickerStart.Value = History_.MinDate;

            dateTimePickerEnd.MaxDate = History_.MaxDate;
            dateTimePickerEnd.MinDate = History_.MinDate;
            dateTimePickerEnd.Value = History_.MaxDate;
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog log_ = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void FormMain_Load(object sender, EventArgs e)
        {
            DbReader_ = new StockMongoDBReader();

            // Load from DB
            IEnumerable<int> allStockId = DbReader_.LoadAllIds();

            int totalStockCount = 0;
            foreach (int id in allStockId)
            {
                comboBoxStockId.Items.Add(id);
                totalStockCount++;
            }
            log_.Info("Form loaded. Stock Count: " + totalStockCount);
        }

        private void comboBoxStockId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(comboBoxStockId.Text, CultureInfo.CurrentCulture);
            IEnumerable<StockData> stocks = DbReader_.Load(id);

            History_.InitAllStocks(id, stocks);
            
            // 刷新界面显示
            ShowStockInfo();
        }

        StockHistory History_ = new StockHistory();

        private void buttonAutoComp_Click(object sender, EventArgs e)
        {
            RunStrategy();
        }

        private void buttonCalcAdjust_Click(object sender, EventArgs e)
        {
            RunStrategyOnMultiStocks();
            //RunStrategy(new SpikeAdjustFactory());
        }

        private void RunStrategy()
        {
            SetUserDefinedDate();
            log_.Info("==>AutoCompare start. Start Date = " + History_.MinDate.ToLongDateString()
                + ", End Date = " + History_.MaxDate.ToLongDateString());

            if (History_.MinDate >= History_.MaxDate)
            {
                log_.Error("Stock histroy not correct!");
                return;
            }

            IStrategyFactory factory = StrategyFactoryCreater.Instance().CreateFactory(StrategyFactoryType.Normal);

            ScoresCalculator calc = new ScoresCalculator();
            calc.Calc(History_, factory, BonusProcessor_);
            calc.ShowResult();
        }

        private void SetUserDefinedDate()
        {
            History_.MaxDate = dateTimePickerEnd.Value;
            History_.MinDate = dateTimePickerStart.Value;
        }

        private void RunStrategyOnMultiStocks()
        {
            // Load from DB
            IEnumerable<int> allStockId = new List<int> { 600238, 600239, 600240, 600007, 
                600008, 600009, 600010,600019,600020,600021,600022};

            StocksHistory histories = new StocksHistory();
            histories.Load(allStockId);

            IStrategyFactory factory = StrategyFactoryCreater.Instance().CreateFactory(StrategyFactoryType.Normal);

            ScoresCalculator calc = new ScoresCalculator();

            foreach (IStockHistory hist in histories.GetAllHistories())
            {                
                calc.Calc(hist, factory, null);
            }

            calc.ShowResult();
        }

        IStockDBReader DbReader_;

        BonusProcessor BonusProcessor_ = new BonusProcessor();

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
            // Check stocks in Database
            WeeklyStatistics stat = new WeeklyStatistics();
            stat.Calc(History_);

            stat.PrintResult(LogMgr.Logger);
        }

        private void buttonChart_Click(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void ShowChart()
        {
            FormMSChart chart = new FormMSChart();

            History_.MaxDate = dateTimePickerEnd.Value;
            History_.MinDate = dateTimePickerStart.Value;
            chart.SetStockDrawer(History_.GetStockDrawer());

            chart.ShowDialog();
        }

        private void buttonHistoryAnalyze_Click(object sender, EventArgs e)
        {
            AnalyseHistory();
        }

        void AnalyseHistory()
        {
            // Load from DB
            IEnumerable<int> allStockId = new List<int> { 600238, 600239, 600240, 600007, 
                600008, 600009, 600010,600019,600020,600021,600022};

            StocksHistory histories = new StocksHistory();
            histories.Load(allStockId);

            StockCharMappingAnalyzer analyzer = new StockCharMappingAnalyzer();

            analyzer.Init(histories);

            int firstId = 0;
            foreach (int stockId in allStockId)
            {
                firstId = stockId;
                break;
            }

            IStockHistory history = histories.GetHistory(firstId);
            if (history == null)
            {
                return;
            }

            IStockHistory oneWeekHistory = history.GetPartStockHistory(history.MaxDate.AddDays(-7),
                history.MaxDate);
            if (oneWeekHistory == null)
            {
                return;
            }

            analyzer.FindMatches(oneWeekHistory);
        }

        private void synchronizeIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockIDSynchronization.Run();
        }
    }
}