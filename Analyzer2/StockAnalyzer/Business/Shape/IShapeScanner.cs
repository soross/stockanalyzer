﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Business.Shape
{
    interface IShapeScanner
    {
        OperType Analyse(IStockData stock, IStockData prevStock);        
    }
}