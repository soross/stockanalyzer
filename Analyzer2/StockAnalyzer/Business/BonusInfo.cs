using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Business
{
    /// <summary>
    /// Bonuses properties of one stock
    /// </summary>
    class BonusInfo
    {
        /// <summary>
        /// Dividend cash
        /// </summary>
        public double Dividend
        {
            get;
            set;
        }

        /// <summary>
        /// Bonus stock hand count
        /// </summary>
        public int BonusCount
        {
            get;
            set;
        }
    }
}
