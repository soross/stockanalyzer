using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using FinanceAnalyzer.Display;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.UI
{
    public partial class FormResultChart : Form
    {
        public FormResultChart()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormResultChart_Load(object sender, EventArgs e)
        {
            InitChart();

            FillData();

            ICollection<string> names = _Results.AllStrategyNames;

            foreach (string strategyname in names)
            {
                DrawResult(strategyname);
            }
        }

        private void DrawResult(string strategyname)
        {
            chart1.Series.Add(strategyname);

            chart1.Series[strategyname].ChartArea = "Result"; // 设置Series绘制区域 
            chart1.Series[strategyname].ChartType = SeriesChartType.Point;
            chart1.Series[strategyname].XValueType = ChartValueType.DateTime;
            chart1.Series[strategyname]["PointWidth"] = "1.5";

            IStockValues values = _Results.GetResult(strategyname);
            if (values == null)
            {
                return;
            }

            ICollection<DateTime> dates = values.GetAllDate();
            foreach (DateTime dt in dates)
            {
                double val = values.GetTotalValue(dt);
                Debug.Assert(val > 0);

                int curPos = chart1.Series[strategyname].Points.AddXY(dt, val);

                if (values.GetOperationSignal(dt) == OperType.Buy)
                {
                    chart1.Series[strategyname].Points[curPos].MarkerImage = ApplicationHelper.GetAppPath() + "\\image\\buysignal.bmp";
                }
                else if (values.GetOperationSignal(dt) == OperType.Sell)
                {
                    chart1.Series[strategyname].Points[curPos].MarkerImage = ApplicationHelper.GetAppPath() + "\\image\\sellsignal.bmp";
                }
            }
        }

        private void InitChart()
        {
            // Set series chart type
            chart1.Series["Price"].ChartType = SeriesChartType.Candlestick;

            // Set the style of the open-close marks
            chart1.Series["Price"]["OpenCloseStyle"] = "Candlestick";

            // Show both open and close marks
            chart1.Series["Price"]["ShowOpenClose"] = "Both";
            chart1.Series["Price"].CustomProperties = "PriceDownColor=Green, PriceUpColor=Red";

            // Set point width
            chart1.Series["Price"]["PointWidth"] = "1.5";

            chart1.ChartAreas["Price"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Price"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["Price"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Price"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Price"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["Price"].CursorY.IsUserSelectionEnabled = true;

            chart1.Series.Add("PriceMA5");
            chart1.Series["PriceMA5"].ChartArea = "Price";
            chart1.Series["PriceMA5"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("PriceMA10");
            chart1.Series["PriceMA10"].ChartArea = "Price";
            chart1.Series["PriceMA10"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("PriceMA20");
            chart1.Series["PriceMA20"].ChartArea = "Price";
            chart1.Series["PriceMA20"].ChartType = SeriesChartType.Line;

            chart1.ChartAreas.Add("Result");
            chart1.ChartAreas["Result"].AlignWithChartArea = "Price";
            chart1.ChartAreas["Result"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Result"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Result"].CursorX.IsUserSelectionEnabled = true;
        }

        private void FillData()
        {
            DateTime startDate = _StockDrawer.MinDate;

            while (startDate < _StockDrawer.MaxDate)
            {
                StockPoint pt = _StockDrawer.GetAt(startDate);

                if (pt != null)
                {
                    int curIdx = chart1.Series["Price"].Points.AddXY(startDate, pt.High);

                    chart1.Series["Price"].Points[curIdx].YValues[1] = pt.Low;

                    chart1.Series["Price"].Points[curIdx].YValues[2] = pt.Open;
                    chart1.Series["Price"].Points[curIdx].YValues[3] = pt.End;
                }

                startDate = startDate.AddDays(1);
                while (Holidays.IsWeekend(startDate))
                {
                    startDate = startDate.AddDays(1);
                }
            }

            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "5", chart1.Series["Price"], chart1.Series["PriceMA5"]);
            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "10", chart1.Series["Price"], chart1.Series["PriceMA10"]);
            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "20", chart1.Series["Price"], chart1.Series["PriceMA20"]);
        }        

        public void SetStrategyResults(IStrategyResults results)
        {
            _Results = results;
        }

        IStrategyResults _Results;

        public void SetStockDrawer(IStockDrawer stockDrawer)
        {
            _StockDrawer = stockDrawer;
        }

        IStockDrawer _StockDrawer;

        private void buttonDetail_Click(object sender, EventArgs e)
        {
            FormStrategyDetail frm = new FormStrategyDetail();
            frm.Results = _Results;
            frm.ShowDialog();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            System.Windows.Forms.DataVisualization.Charting.Cursor curCursor = chart1.ChartAreas["Price"].CursorX;

            switch (keyData)
            {
                case Keys.Left:
                    curCursor.SetCursorPosition(curCursor.Position - curCursor.Interval);
                    break;
                case Keys.Right:
                    curCursor.SetCursorPosition(curCursor.Position + curCursor.Interval);
                    break;
                default:
                    break;
            }

            return true;
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {

        }
    }
}
