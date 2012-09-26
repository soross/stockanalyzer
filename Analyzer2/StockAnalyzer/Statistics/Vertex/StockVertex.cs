using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Statistics.Vertex
{
    enum VertexType
    {
        Max,
        Min
    };

    enum VertexFindType
    {
        Manual,
        Automatic
    };

    class StockVertex
    {
        public VertexType VertType
        {
            get;
            set;
        }

        public DateTime VertexDate
        {
            get;
            set;
        }

        public VertexFindType FindType
        {
            get;
            set;
        }

        public string ToString()
        {
            return "Date: " + VertexDate.ToShortDateString() + ", Type: " + VertType;
        }
    }
}
