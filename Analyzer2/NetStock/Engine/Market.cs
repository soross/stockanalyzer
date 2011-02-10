using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public enum ChangeType
    {
        Up,
        Down,
        Unchange
    }

    public interface Market
    {
        double getIndex(Index index);
        double getChange(Index index);
        int getNumOfStockChange(ChangeType type);
        long getVolume();
        double getValue();
        Country getCountry();        
    }
}
