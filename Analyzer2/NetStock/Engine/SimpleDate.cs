using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public class SimpleDate
    {
        /** Creates a new instance of SimpleDate */
        // month: 0-based, 0 means Jan. 
        public SimpleDate(int year, int month, int date)
        {
            CurrentDate = new DateTime(year, month + 1, date);
        }

        public SimpleDate()
        {
            CurrentDate = DateTime.Now;
        }

        public SimpleDate(SimpleDate simpleDate)
        {
            this.CurrentDate = simpleDate.CurrentDate;
        }

        // Adapter
        public SimpleDate(DateTime simpleDate)
        {
            this.CurrentDate = simpleDate;
        }

        // Helper
        public DateTime CurrentDate
        {
            get;
            set;
        }

        public int getYear()
        {
            return CurrentDate.Year;
        }

        // 0-based
        public int getMonth()
        {
            return CurrentDate.Month - 1;
        }

        public int getDate()
        {
            return CurrentDate.Day;
        }

        public int compareTo(SimpleDate simpleDate)
        {
            if (this.CurrentDate > simpleDate.CurrentDate)
            {
                return 1;
            }
            else if (this.CurrentDate == simpleDate.CurrentDate)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public String toString()
        {
            return "SimpleDate" + "[year=" + CurrentDate.Year + ",month=" + CurrentDate.Month + ",date=" + CurrentDate.Day + "]";
        }
    }
}
