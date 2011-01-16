using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FinanceAnalyzer.Display;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.UI
{
    public partial class FormMSChart : Form
    {
        public FormMSChart()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMSChart_Load(object sender, EventArgs e)
        {
            InitChart();

            FillData();
        }

        private void InitChart()
        {
            // Set series chart type
            chart1.Series["Price"].ChartType = SeriesChartType.Candlestick;

            // Set the style of the open-close marks
            chart1.Series["Price"]["OpenCloseStyle"] = "Candlestick";

            // Show both open and close marks
            chart1.Series["Price"]["ShowOpenClose"] = "Both";

            // Set point width
            chart1.Series["Price"]["PointWidth"] = "0.8";

            chart1.ChartAreas["Price"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Price"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Price"].CursorX.IsUserSelectionEnabled = true;

            chart1.ChartAreas.Add("Volume");
            chart1.ChartAreas["Volume"].AlignWithChartArea = "Price";
            chart1.ChartAreas["Volume"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Volume"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Volume"].CursorX.IsUserSelectionEnabled = true;

            chart1.Series.Add("Volume");
            chart1.Series["Volume"].ChartArea = "Volume";
            chart1.Series["Volume"].ChartType = SeriesChartType.Column;
            chart1.Series["Volume"].XValueType = ChartValueType.DateTime;
        }

        private void FillData()
        {
            DateTime startDate = _StockDrawer.MinDate;

            double minYValue = Double.MaxValue;
            while (startDate < _StockDrawer.MaxDate)
            {
                StockPoint pt = _StockDrawer.GetAt(startDate);

                if (pt != null)
                {
                    int curIdx = chart1.Series["Price"].Points.AddXY(startDate, pt.High);
                    chart1.Series["Volume"].Points.AddXY(startDate, pt.Volume);

                    chart1.Series["Price"].Points[curIdx].YValues[1] = pt.Low;

                    chart1.Series["Price"].Points[curIdx].YValues[2] = pt.Open;
                    chart1.Series["Price"].Points[curIdx].YValues[3] = pt.End;

                    minYValue = (minYValue > pt.Low) ? pt.Low : minYValue;
                }

                startDate = startDate.AddDays(1);
                while (Holidays.IsWeekend(startDate))
                {
                    startDate = startDate.AddDays(1);
                }
            }

            if (minYValue != Double.MaxValue)
            {
                chart1.ChartAreas["Price"].AxisY.Minimum = (int)minYValue - 1; // 最小值设置
            }
        }

        public void SetStockDrawer(IStockDrawer stockDrawer)
        {
            _StockDrawer = stockDrawer;
        }

        IStockDrawer _StockDrawer;
    }
}
