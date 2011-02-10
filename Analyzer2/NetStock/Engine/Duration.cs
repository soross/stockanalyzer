using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public class Duration
    {
        public Duration(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("startDate " + startDate + " should not after endDate " + endDate);
            }

            this.startDate = startDate;
            this.endDate = endDate;
        }

        public DateTime getStartDate()
        {
            return startDate;
        }

        public DateTime getEndDate()
        {
            return endDate;
        }

        public long getDurationInDays()
        {
            TimeSpan span = endDate - startDate;
            durationInDays = span.Days;
            return durationInDays;
        }

        public bool isContains(Duration duration)
        {
            return (this.startDate <= duration.startDate) && (this.endDate >= duration.endDate);
        }

        public static Duration getTodayDurationByYears(int durationInYears)
        {
            if (durationInYears < 0)
            {
                throw new ArgumentException("durationInYears must be a non-negative number");
            }

            DateTime start = DateTime.Now;
            DateTime end = start.AddYears(durationInYears);
            return new Duration(start, end);
        }

        public Duration getUnionDuration(Duration duration)
        {
            DateTime start = (this.startDate <= duration.getStartDate()) ? this.startDate : duration.getStartDate();
            DateTime end = this.endDate >= duration.getEndDate() ? this.endDate : duration.getEndDate();

            return new Duration(start, end);
        }

        public static Duration getTodayDurationByDays(int durationInDays)
        {
            if (durationInDays < 0)
            {
                throw new ArgumentException("durationInDays must be a non-negative number");
            }

            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(durationInDays);
            return new Duration(start, end);
        }

        public String toString()
        {
            return "Duration" + "[startDate=" + startDate + ",endDate=" + endDate + "]";
        }

        // Cache
        private long durationInDays = -1;
        private DateTime startDate;
        private DateTime endDate;
    }
}


