using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    

    class IndustryHelper
    {
        private IndustryHelper()
        {
            _EnumNames.Add(Stock.Industry.ConsumerProducts, "Consumer Products");      // CONSUMER
            _EnumNames.Add(Stock.Industry.IndustrialProducts, "Industrial Products");  // IND-PROD
            _EnumNames.Add(Stock.Industry.Construction, "Construction");       // CONSTRUCTN
            _EnumNames.Add(Stock.Industry.TradingServices, "Trading / Services");      // TRAD/SERV
            _EnumNames.Add(Stock.Industry.Technology, "Technology");   // TECHNOLOGY
            _EnumNames.Add(Stock.Industry.Infrastructure, "Infrastructure");   // IPC
            _EnumNames.Add(Stock.Industry.Finance, "Finance"); // FINANCE
            _EnumNames.Add(Stock.Industry.Hotels, "Hotels");   // HOTELS
            _EnumNames.Add(Stock.Industry.Properties, "Properties");   // PROPERTIES 
            _EnumNames.Add(Stock.Industry.Plantation, "Plantation");   // PLANTATION
            _EnumNames.Add(Stock.Industry.Mining, "Mining");   // MINING
            _EnumNames.Add(Stock.Industry.Trusts, "Trusts");   // REITS
            _EnumNames.Add(Stock.Industry.CloseEndFund, "Close-End Fund");     // CLOSED/FUND 
            _EnumNames.Add(Stock.Industry.ETF, "ETF"); // ETF
            _EnumNames.Add(Stock.Industry.Loans, "Loans");     // LOANS
            _EnumNames.Add(Stock.Industry.CallWarrant, "Call Warrant");// CALL-WARR
            _EnumNames.Add(Stock.Industry.UserDefined, "User Defined");
            _EnumNames.Add(Stock.Industry.Unknown, "Unknown");
        }

        public static IndustryHelper Instance()
        {
            return _Instance;
        }

        static IndustryHelper _Instance = new IndustryHelper();

        Dictionary<Stock.Industry, string> _EnumNames = new Dictionary<Stock.Industry, string>();
    }
}
