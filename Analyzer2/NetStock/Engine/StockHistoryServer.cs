using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public interface StockHistoryServer
    {
        Stock getStock(DateTime calendar);
        DateTime getCalendar(int index);
        int getNumOfCalendar();

        // Currently, we didn't see the need for sharesIssued and marketCapital to
        // become the member variables of Stock. In order to avoid memory hungry
        // monster and difficulty in constructing Stock, we will move the
        // share issued and market capital information into here.
        //
        // Currently, I am still not sure whether this is a good idea. We need
        // to experiment it out.
        long getSharesIssued();
        long getMarketCapital();
    }
}
