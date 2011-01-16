using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class FinanceAnalyzer
    {
        [Test]
        public void GetPrevWorkDay()
        {
            DateTime dt = new DateTime(2009, 3, 21);
            DateTime prev = DateFunc.GetPreviousWorkday(dt);

            Assert.AreEqual(prev, new DateTime(2009, 3, 20));
        }

        [Test]
        public void GetPrevWorkDay2()
        {
            DateTime dt = new DateTime(2009, 3, 22);
            DateTime prev = DateFunc.GetPreviousWorkday(dt);

            Assert.AreEqual(prev, new DateTime(2009, 3, 20));
        }

        [Test]
        public void GetNextWorkDay()
        {
            DateTime dt = new DateTime(2009, 3, 21);
            DateTime prev = DateFunc.GetNextWorkday(dt);

            Assert.AreEqual(prev, new DateTime(2009, 3, 23));
        }

        [Test]
        public void GetNextWorkDay2()
        {
            DateTime dt = new DateTime(2009, 3, 22);
            DateTime prev = DateFunc.GetNextWorkday(dt);

            Assert.AreEqual(prev, new DateTime(2009, 3, 23));
        }

        [Test]
        public void ParseString()
        {
            DateTime dt = DateFunc.ParseString("20090320");

            Assert.AreEqual(dt, new DateTime(2009, 3, 20));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseStringErrFmt()
        {
            DateTime dt = DateFunc.ParseString("NA");
        }

        [Test]
        public void IsHoliday()
        {
            Assert.IsTrue(DateFunc.IsHoliday(new DateTime(2008, 5, 1)));
            Assert.IsTrue(DateFunc.IsHoliday(new DateTime(2008, 5, 3)));
            Assert.IsFalse(DateFunc.IsHoliday(new DateTime(2008, 5, 7)));

            Assert.IsTrue(DateFunc.IsHoliday(new DateTime(2008, 10, 1)));
            Assert.IsTrue(DateFunc.IsHoliday(new DateTime(2008, 10, 3)));
            Assert.IsFalse(DateFunc.IsHoliday(new DateTime(2008, 10, 7)));
        }

        [Test]
        public void IsHolidayWeekend()
        {
            Assert.IsFalse(DateFunc.IsHoliday(new DateTime(2009, 3, 20)));
            Assert.IsTrue(DateFunc.IsHoliday(new DateTime(2009, 3, 21)));
            Assert.IsTrue(DateFunc.IsHoliday(new DateTime(2009, 3, 22)));
            Assert.IsFalse(DateFunc.IsHoliday(new DateTime(2009, 3, 23)));
        }
    }
}
