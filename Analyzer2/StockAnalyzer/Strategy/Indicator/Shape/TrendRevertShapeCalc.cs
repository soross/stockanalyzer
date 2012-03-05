using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
{
    /// <summary>
    /// 试图找出顶点
    /// </summary>
    class TrendRevertShapeCalc : BasicIndicatorCalc
    {
        public override void Calc(IStockHistory hist)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return "Vertex"; }
        }

        static const int SLOPE_DAYS = 7;
        static const int SLOPE_OFFSET_DAYS = 3;
    }
}
