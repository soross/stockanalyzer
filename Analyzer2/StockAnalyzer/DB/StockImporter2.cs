using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Stock.Common.Data; 

namespace FinanceAnalyzer.DB
{
    using Excel = Microsoft.Office.Interop.Excel;
    using System.Reflection;
    using System.Diagnostics;
    using System.Globalization;
    using FinanceAnalyzer.Log;
    using FinanceAnalyzer.Utility;    

    public class StockImporter2
    {
        public void SetStockSaver(IStockSaver stockSaver)
        {
            _StockDBSaver = stockSaver;
        }

        private IStockSaver _StockDBSaver;

        // 从文件加载
        public void Import(string fileName)
        {
            if (_StockDBSaver == null)
            {
                _log.Error("File Import: Saver not init!!!");
                return;
            }

            try
            {
                Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook oWB = oXL.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, 
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                Excel._Worksheet sheet = (Excel._Worksheet)oWB.Sheets[1]; // 1-based

                Excel.Range rge = sheet.UsedRange;
                //
                // Take the used range of the sheet. Finally, get an object array of all
                // of the cells in the sheet (their values). You can do things with those
                // values. See notes about compatibility.
                //
                object[,] valueArray = (object[,])rge.get_Value(
                    Excel.XlRangeValueDataType.xlRangeValueDefault);

                //Determine the dimensions of the array.
                long iRows = valueArray.GetUpperBound(0);
                long iCols = valueArray.GetUpperBound(1);

                if ((iRows <= 2) || (iCols <= 8))
                {
                    oXL.Workbooks.Close();
                    return;
                }

                _StockDBSaver.BeforeAdd();

                const int STARTVAL = 1; // 数组或单元格的起始值

                for (int i = STARTVAL + 2; i < iRows; i++)
                {
                    StockData data = new StockData();
                    data.Id = Convert.ToInt32(valueArray[STARTVAL, STARTVAL + 1], CultureInfo.CurrentCulture);
                    data.TradeDate = DateFunc.ParseString(valueArray[i, STARTVAL]);
                    data.StartPrice = Convert.ToDouble(valueArray[i, STARTVAL + 1], CultureInfo.CurrentCulture);
                    data.MaxPrice = Convert.ToDouble(valueArray[i, STARTVAL + 2], CultureInfo.CurrentCulture);
                    data.MinPrice = Convert.ToDouble(valueArray[i, STARTVAL + 3], CultureInfo.CurrentCulture);
                    data.EndPrice = Convert.ToDouble(valueArray[i, STARTVAL + 4], CultureInfo.CurrentCulture);
                    data.VolumeHand = Convert.ToInt32(valueArray[i, STARTVAL + 5], CultureInfo.CurrentCulture);
                    data.Amount = Convert.ToDouble(valueArray[i, STARTVAL + 6], CultureInfo.CurrentCulture);

                    // 保存
                    _StockDBSaver.Add(data);
                }

                _StockDBSaver.AfterAdd();
                _log.Info("Import complete!");
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                LogMgr.Logger.LogInfo(errorMessage, "Error");
            }
        }

        // 从文件加载
        public void ImportYahoo(string fileName)
        {
            if (_StockDBSaver == null)
            {
                _log.Error("File Import: Saver not init!!!");
                return;
            }

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                
                // 分析股票代码
                int stockID = -1;
                XmlNode idNode = doc.SelectSingleNode("//code");

                if (idNode != null)
                {
                    string str = idNode.InnerText;
                    string[] arr = str.Split('.');

                    if (arr.Length <= 1)
                    {
                        stockID = int.Parse(str, CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        stockID = int.Parse(arr[0], CultureInfo.CurrentCulture);
                    }
                }

                XmlNodeList nodeList = doc.SelectNodes("//item");

                if (nodeList.Count == 0)
                {
                    return;
                }

                _StockDBSaver.BeforeAdd();

                foreach (XmlNode item in nodeList)
                {

                    StockData data = new StockData();
                    data.Id = stockID;

                    GetStockData(item, ref data);
                    // 保存
                    _StockDBSaver.Add(data);
                }

                _StockDBSaver.AfterAdd();
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                LogMgr.Logger.LogInfo(errorMessage, "Error");
            }
        }

        private static void GetStockData(XmlNode node, ref StockData data)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "date":
                        data.TradeDate = DateFunc.ParseString(child.InnerText);
                        break;
                    case "open":
                        data.StartPrice = double.Parse(child.InnerText, CultureInfo.CurrentCulture);
                        break;
                    case "high":
                        data.MaxPrice = double.Parse(child.InnerText, CultureInfo.CurrentCulture);
                        break;
                    case "low":
                        data.MinPrice = double.Parse(child.InnerText, CultureInfo.CurrentCulture);
                        break;
                    case "close":
                        data.EndPrice = double.Parse(child.InnerText, CultureInfo.CurrentCulture);
                        break;
                    case "volume":
                        data.VolumeHand = Convert.ToInt32(double.Parse(child.InnerText, CultureInfo.CurrentCulture) * 100); // 数据文件的单位为万股 
                        break;
                    case "amount":
                        data.Amount = double.Parse(child.InnerText, CultureInfo.CurrentCulture); // 单位：万元 
                        break;
                }
            }
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
