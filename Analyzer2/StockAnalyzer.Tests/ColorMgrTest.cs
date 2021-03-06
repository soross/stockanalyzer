// <copyright file="ColorMgrTest.cs" company="huawei">版权所有 (C) huawei 2008</copyright>

using System;
using FinanceAnalyzer;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using System.Drawing;

namespace FinanceAnalyzer
{
    [TestFixture]
    [PexClass(typeof(ColorMgr))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ColorMgrTest
    {
        [PexMethod]
        public Color DiffColor([PexAssumeUnderTest]ColorMgr target)
        {
            // TODO: add assertions to method ColorMgrTest.DiffColor(ColorMgr)
            Color result = target.DiffColor();
            return result;
        }
    }
}
