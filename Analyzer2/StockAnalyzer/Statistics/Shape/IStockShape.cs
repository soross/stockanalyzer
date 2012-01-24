using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Statistics.Shape
{
    enum StockShapeEnum
    {
        ThreeDaysUp,
        TwoDaysUp,
        ThreeDaysVolumeUp,
        TwoDaysVolumeUp,

        ThreeDaysDown,
        TwoDaysDown,
        ThreeDaysVolumeDown,
        TwoDaysVolumeDown,
    }

    interface IStockShape
    {
    }
}
