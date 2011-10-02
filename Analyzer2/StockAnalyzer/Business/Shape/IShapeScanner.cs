using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business.Shape
{
    interface IShapeScanner
    {
        OperType Analyse(IStockData stock, IStockData prevStock);        
    }
}
