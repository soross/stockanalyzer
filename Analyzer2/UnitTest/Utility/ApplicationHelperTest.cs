using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FinanceAnalyzer.Utility
{
    [TestFixture]
    public class ApplicationHelperTest
    {
        [Test]
        public void Test()
        {
            Assert.IsNotNullOrEmpty(ApplicationHelper.GetAppPath());
        }
    }
}
