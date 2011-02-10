using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    /**
 * This class is used to provide stock code to stock name mapping, as some
 * stock server doesn't return good stock name. For example, China users wish
 * to view stock name in Chinese. However, Yahoo! Stock Server returns stock
 * name in English. In this case, we need to retrieve stock name from
 * <code>StockNameDatabase</code>.
 */
    class StockNameDatabase
    {
        /**
     * Initializes a newly created {@code StockNameDatabase} object so
     * that it contains stock code to stock name mapping information. The
     * information is being retrieved from list of stocks.
     *
     * @param stocks List of stocks which provides stock information
     */
        public StockNameDatabase(List<Stock> stocks)
        {
            foreach (Stock stock in stocks)
            {
                _codeToName.Add(stock.getCode(), stock.getName());
            }
        }

        /**
         * Returns name based on the code.
         * @param code The code
         * @return name based on the code. <code>null</code> will be returned if
         * no match found.
         */
        public String codeToName(Code code)
        {
            return _codeToName[code];
        }

        private Dictionary<Code, String> _codeToName = new Dictionary<Code, String>();
    }
}
