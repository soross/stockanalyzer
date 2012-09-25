using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Statistics.Vertex
{
    class Vertexes
    {
        public void Add(StockVertex sv)
        {
            if (sv == null)
            {
                return;
            }

            if (vertexes_.ContainsKey(sv.VertexDate))
            {
                return;
            }

            vertexes_.Add(sv.VertexDate, sv);
        }

        public ICollection<StockVertex> GetAll()
        {
            return vertexes_.Values;
        }

        Dictionary<DateTime, StockVertex> vertexes_ = new Dictionary<DateTime, StockVertex>();
        //List<StockVertex> vertexes_ = new List<StockVertex>();
    }
}
