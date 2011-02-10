using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public class Symbol
    {
        private Symbol(String symbol)
        {
            this.symbol = symbol;
        }

        public static Symbol newInstance(String symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentException("symbol cannot be null");
            }

            return new Symbol(symbol);
        }

        public String toString()
        {
            return symbol;
        }

        private String symbol;
    }
}
