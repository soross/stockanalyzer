using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Stock
{
    class HolderManager
    {
        private HolderManager()
        {
        }

        static HolderManager _Instance = new HolderManager();

        public static HolderManager Instance()
        {
            return _Instance;
        }

        public IStockHolder Holder
        {
            get;
            set;
        }
    }
}
