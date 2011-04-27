﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    class FakeIBonusReader : IBonusReader
    {
        public IList<Bonus> Query(int stockId)
        {
            return null;
        }

        public int Count()
        {
            return 0;
        }
    }
}
