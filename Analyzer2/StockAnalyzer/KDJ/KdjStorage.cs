using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.KDJ
{
    class KdjStorage : IKdjStorage
    {
        public double GetK(DateTime dt)
        {
            DateTime currentDate = dt;
            while ((!DateKs_.ContainsKey(currentDate)) && (currentDate > MinDate))
            {
                currentDate = DateFunc.GetPreviousWorkday(currentDate);
            }

            if (DateKs_.ContainsKey(currentDate))
            {
                return DateKs_[currentDate];
            }
            else
            {
                return 50; // 无效值
            }
        }

        public double GetD(DateTime dt)
        {
            DateTime currentDate = dt;
            while ((!DateDs_.ContainsKey(currentDate)) && (currentDate > MinDate))
            {
                currentDate = DateFunc.GetPreviousWorkday(currentDate);
            }

            if (DateDs_.ContainsKey(currentDate))
            {
                return DateDs_[currentDate];
            }
            else
            {
                return 50; // 无效值
            }
        }

        public double GetJ(DateTime dt)
        {
            return (3 * GetD(dt)) + (2 * GetK(dt));
        }

        public void SetK(DateTime dt, double k)
        {
            DateKs_.Add(dt, k);
        }

        public void SetD(DateTime dt, double d)
        {
            DateDs_.Add(dt, d);
        }

        public DateTime MinDate
        {
            get;
            set;
        }

        public DateTime MaxDate
        {
            get;
            set;
        }

        private Dictionary<DateTime, double> DateKs_ = new Dictionary<DateTime, double>();
        private Dictionary<DateTime, double> DateDs_ = new Dictionary<DateTime, double>();
    }
}
