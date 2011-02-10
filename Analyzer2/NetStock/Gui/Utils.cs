using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStock.Engine;

namespace DotNetStock.Gui
{
    class Utils
    {
        public static Stock getEmptyStock(Code code, Symbol symbol)
        {
            return new Stock(code,
                                symbol,
                                "",
                                Stock.Board.Unknown,
                                Stock.Industry.Unknown,
                                0.0,
                                0.0,
                                0.0,
                                0.0,
                                0.0,
                                0,
                                0.0,
                                0.0,
                                0,
                                0.0,
                                0,
                                0.0,
                                0,
                                0.0,
                                0,
                                0.0,
                                0,
                                0.0,
                                0,
                                0.0,
                                0,
                                DateTime.MinValue
                                );
        }

        public static String getResponseBodyAsStringBasedOnProxyAuthOption(String request)
        {
            return "";
        }
    }
}
