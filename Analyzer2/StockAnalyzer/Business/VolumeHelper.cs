using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business
{
    class VolumeHelper
    {
        public static bool IsLargerThan(IStockData curData, IStockData prevData, double ratio)
        {
            return curData.Amount > (prevData.Amount * (1 + ratio));
        }
    }
}
