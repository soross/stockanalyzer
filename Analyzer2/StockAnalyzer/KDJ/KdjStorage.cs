using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.KDJ
{
    class KdjStorage : IKdjStorage
    {
        public double GetK(DateTime dt)
        {
            if (_DateKs.ContainsKey(dt))
            {
                return _DateKs[dt];
            }
            else
            {
                return -1; // 无效值
            }
        }

        public double GetD(DateTime dt)
        {
            if (_DateDs.ContainsKey(dt))
            {
                return _DateDs[dt];
            }
            else
            {
                return -1; // 无效值
            }
        }

        public double GetJ(DateTime dt)
        {
            return (3 * GetD(dt)) + (2 * GetK(dt));
        }

        public void SetK(DateTime dt, double k)
        {
            _DateKs.Add(dt, k);
        }

        public void SetD(DateTime dt, double d)
        {
            _DateDs.Add(dt, d);
        }

        private Dictionary<DateTime, double> _DateKs = new Dictionary<DateTime, double>();
        private Dictionary<DateTime, double> _DateDs = new Dictionary<DateTime, double>();
    }
}
