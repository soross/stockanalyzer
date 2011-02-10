using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public interface MarketServer
    {
        Market getMarket();
    }
}
