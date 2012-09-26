using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Statistics.Vertex
{
    class VertexJudger
    {
        public ICollection<StockVertex> FindVertex(IStockHistory hist)
        {
            Dictionary<int, StockVertex> datePosToVertex = new Dictionary<int, StockVertex>();
            //List<StockVertex> vertexes = new List<StockVertex>();
            Vertexes vertexes = new Vertexes();

            //FixedSizeLinkedList<IStockData> fixedvertexes = new FixedSizeLinkedList<IStockData>(TIME_WINDOW_MARGIN);
            FixedSizeLinkedList<SortStock> fixedvertexes = new FixedSizeLinkedList<SortStock>(TIME_WINDOW_MARGIN);
            FixedSizeLinkedList<SortStock> fixedvertexesmin = new FixedSizeLinkedList<SortStock>(TIME_WINDOW_MARGIN);

            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            int currentIdx = 0;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);

                if (stock != null)
                {
                    fixedvertexes.AddLast(new SortStock(currentIdx, startDate, stock.MaxPrice));
                    fixedvertexesmin.AddLast(new SortStock(currentIdx, startDate, stock.MinPrice));
                }

                if (fixedvertexes.IsEnough())
                {
                    SortStock stockMax = fixedvertexes.FindMax();
                    SortStock stockMin = fixedvertexesmin.FindMin();

                    vertexes.Add(CreateVertex(stockMax, VertexType.Max));
                    vertexes.Add(CreateVertex(stockMin, VertexType.Min));
                }

                startDate = DateFunc.GetNextWorkday(startDate);
                currentIdx++;
            }

            return vertexes.GetAll();
        }

        static StockVertex CreateVertex(SortStock sd, VertexType vtp)
        {
            if (sd == null)
            {
                return null;
            }

            StockVertex sv = new StockVertex();
            sv.FindType = VertexFindType.Automatic;
            sv.VertType = vtp;
            sv.VertexDate = sd.CurrentDate;

            return sv;
        }

        const int TIME_WINDOW_MARGIN = 10;
    }
}
