using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class BoardHelper
    {
        public string GetBoardName(Stock.Board param)
        {
            if (_BoardNames.ContainsKey(param))
            {
                return _BoardNames[param];
            }
            else
            {
                return "Unknown";
            }
        }

        private BoardHelper()
        {
            _BoardNames.Add(Stock.Board.Main, "Main Board");     // Main
            _BoardNames.Add(Stock.Board.Second, "Second Board"); // 2nd
            _BoardNames.Add(Stock.Board.Mesdaq, "Mesdaq");       // MESDAQ
            _BoardNames.Add(Stock.Board.CallWarrant, "Call Warrant");    // ??
            _BoardNames.Add(Stock.Board.KualaLumpur, "Kuala Lumpur");
            _BoardNames.Add(Stock.Board.SES, "SES");     // Singapore
            _BoardNames.Add(Stock.Board.Copenhagen, "Copenhagen");       // Denmark
            _BoardNames.Add(Stock.Board.Paris, "Paris"); // France
            _BoardNames.Add(Stock.Board.Xetra, "Xetra"); // Germany
            _BoardNames.Add(Stock.Board.XETRA, "XETRA");
            _BoardNames.Add(Stock.Board.Munich, "Munich");
            _BoardNames.Add(Stock.Board.Stuttgart, "Stuttgart");
            _BoardNames.Add(Stock.Board.Berlin, "Berlin");
            _BoardNames.Add(Stock.Board.Hamburg, "Hamburg");
            _BoardNames.Add(Stock.Board.Dusseldorf, "Dusseldorf");
            _BoardNames.Add(Stock.Board.Frankfurt, "Frankfurt");
            _BoardNames.Add(Stock.Board.Hannover, "Hannover");
            _BoardNames.Add(Stock.Board.Milan, "Milan"); // Italy
            _BoardNames.Add(Stock.Board.Oslo, "Oslo");   // Norway
            _BoardNames.Add(Stock.Board.Madrid, "Madrid");       // Spain
            _BoardNames.Add(Stock.Board.MCE, "MCE");
            _BoardNames.Add(Stock.Board.MercadoContinuo, "Mercado Continuo");
            _BoardNames.Add(Stock.Board.Stockholm, "Stockholm"); // Sweden
            _BoardNames.Add(Stock.Board.FSI, "FSI");     // UK
            _BoardNames.Add(Stock.Board.London, "London");
            _BoardNames.Add(Stock.Board.NasdaqSC, "NasdaqSC");   // US
            _BoardNames.Add(Stock.Board.DJI, "DJI");
            _BoardNames.Add(Stock.Board.NasdaqNM, "NasdaqNM");
            _BoardNames.Add(Stock.Board.NYSE, "NYSE");
            _BoardNames.Add(Stock.Board.Nasdaq, "Nasdaq");
            _BoardNames.Add(Stock.Board.AMEX, "AMEX");
            _BoardNames.Add(Stock.Board.PinkSheet, "Pink Sheet");
            _BoardNames.Add(Stock.Board.Sydney, "Sydney");       // Australia
            _BoardNames.Add(Stock.Board.ASX, "ASX");
            _BoardNames.Add(Stock.Board.Vienna, "Vienna");       // Austria
            _BoardNames.Add(Stock.Board.Brussels, "Brussels");   // Belgium
            _BoardNames.Add(Stock.Board.Toronto, "Toronto");     // Canada
            _BoardNames.Add(Stock.Board.HKSE, "HKSE");   // Hong Kong
            _BoardNames.Add(Stock.Board.Jakarta, "Jakarta");     // Indonesia
            _BoardNames.Add(Stock.Board.KSE, "KSE");     // Korea
            _BoardNames.Add(Stock.Board.KOSDAQ, "KOSDAQ");
            _BoardNames.Add(Stock.Board.Amsterdam, "Amsterdam"); // Netherlands
            _BoardNames.Add(Stock.Board.ENX, "ENX");     // Portugal
            _BoardNames.Add(Stock.Board.Lisbon, "Lisbon");
            _BoardNames.Add(Stock.Board.VTX, "VTX");     // Switzerland
            _BoardNames.Add(Stock.Board.Virt_X, "Virt-X");
            _BoardNames.Add(Stock.Board.Switzerland, "Switzerland");
            _BoardNames.Add(Stock.Board.Taiwan, "Taiwan");       // Taiwan
            _BoardNames.Add(Stock.Board.Bombay, "Bombay");       // India
            _BoardNames.Add(Stock.Board.NSE, "NSE");
            _BoardNames.Add(Stock.Board.UserDefined, "User Defined");
            _BoardNames.Add(Stock.Board.Unknown, "Unknown");
        }

        public static BoardHelper Instance()
        {
            return _Instance;
        }

        static BoardHelper _Instance = new BoardHelper();

        Dictionary<Stock.Board, string> _BoardNames = new Dictionary<Stock.Board, string>();
    };
}
