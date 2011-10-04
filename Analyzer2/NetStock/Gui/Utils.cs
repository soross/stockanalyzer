using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStock.Engine;
using System.Net;
using System.IO;

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
                                new SimpleDate()
                                );
        }

        public static String getResponseBodyAsStringBasedOnProxyAuthOption(String request)
        {
            WebRequest req = WebRequest.Create(request);
            WebResponse result = req.GetResponse();
            Stream ReceiveStream = result.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(ReceiveStream, encode);

            string response = sr.ReadToEnd();
            return response;
        }
    }
}
