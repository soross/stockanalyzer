using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Stock.Common.Data; 

namespace FinanceAnalyzer.DB
{
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
                    data.StockId = stockID;

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
