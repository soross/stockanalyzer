using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockDownloader.Log;
using StockDownloader.Business;

namespace StockDownloader
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            string[] arr = textBoxStockIDs.Text.Split(new char[] { ' ', ',' }, 
                StringSplitOptions.RemoveEmptyEntries);
            List<int> stockIds = new List<int>();
            foreach (string s in arr)
            {
                try
                {
                    int val = int.Parse(s);
                    stockIds.Add(val);
                }
                catch (FormatException ex)
                {
                    LogManager.GetInstance().Log(ex.Message);
                }
            }

            StocksDownloader downloader = new StocksDownloader();
            downloader.Download(stockIds);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LogManager.GetInstance().UILog = listBoxLog;
        }
    }
}
