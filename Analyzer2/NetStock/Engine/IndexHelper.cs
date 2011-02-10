using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    enum Index
    {
        KLSE,
        Second,
        Mesdaq,
        STI,
        DJI,
        IXIC,
        DAX,
        OMXSPI,
        OMXC20CO,
        OSEAX,
        SPMIB,
        SMSI,
        FTSE,
        FCHI,
        BSESN,
        NSEI,
        AORD,
        ATX,
        BFX,
        GSPTSE,
        HSI,
        JKSE,
        KS11,
        AEX,
        PSI20,
        TWII,
        SSMI,
        BVSP,
        SSEC
    };

    class IndexHelper
    {
        private IndexHelper()
        {
            _EnumNames.Add(Index.KLSE, "KLSE");
            _EnumNames.Add(Index.Second, "Second Board");
            _EnumNames.Add(Index.Mesdaq, "Mesdaq");
            _EnumNames.Add(Index.STI, "Straits Times Index");
            _EnumNames.Add(Index.DJI, "Dow Jones Industrial Average");
            _EnumNames.Add(Index.IXIC, "Nasdaq Composite");
            _EnumNames.Add(Index.DAX, "DAX");
            _EnumNames.Add(Index.OMXSPI, "Stockholm General");
            _EnumNames.Add(Index.OMXC20CO, "OMX Copenhagen 20");
            _EnumNames.Add(Index.OSEAX, "OSE All Share");
            _EnumNames.Add(Index.SPMIB, "S&P/MIB");
            _EnumNames.Add(Index.SMSI, "Madrid General");
            _EnumNames.Add(Index.FTSE, "FTSE 100");
            _EnumNames.Add(Index.FCHI, "CAC 40");
            _EnumNames.Add(Index.BSESN, "BSE SENSEX");
            _EnumNames.Add(Index.NSEI, "S&P CNX NIFTY");
            _EnumNames.Add(Index.AORD, "All Ordinaries");
            _EnumNames.Add(Index.ATX, "ATX");
            _EnumNames.Add(Index.BFX, "BEL-20");
            _EnumNames.Add(Index.GSPTSE, "S&P TSX Composite");
            _EnumNames.Add(Index.HSI, "Hang Seng");
            _EnumNames.Add(Index.JKSE, "Jakarta Composite");
            _EnumNames.Add(Index.KS11, "Seoul Composite");
            _EnumNames.Add(Index.AEX, "AEX");
            _EnumNames.Add(Index.PSI20, "PSI 20");
            _EnumNames.Add(Index.TWII, "TSEC weighted index");
            _EnumNames.Add(Index.SSMI, "Swiss Market");
            _EnumNames.Add(Index.BVSP, "Bovespa");
            _EnumNames.Add(Index.SSEC, "China Shanghai Composite");

            _EnumCodes.Add(Index.KLSE, Code.newInstance("^KLSE"));
            _EnumCodes.Add(Index.Second, Code.newInstance("Second"));
            _EnumCodes.Add(Index.Mesdaq, Code.newInstance("Mesdaq"));
            _EnumCodes.Add(Index.STI, Code.newInstance("^STI"));
            _EnumCodes.Add(Index.DJI, Code.newInstance("^DJI"));
            _EnumCodes.Add(Index.IXIC, Code.newInstance("^IXIC"));
            _EnumCodes.Add(Index.DAX, Code.newInstance("^GDAXI"));
            _EnumCodes.Add(Index.OMXSPI, Code.newInstance("^OMXSPI"));
            _EnumCodes.Add(Index.OMXC20CO, Code.newInstance("OMXC20.CO"));
            _EnumCodes.Add(Index.OSEAX, Code.newInstance("^OSEAX"));
            _EnumCodes.Add(Index.SPMIB, Code.newInstance("^SPMIB"));
            _EnumCodes.Add(Index.SMSI, Code.newInstance("^SMSI"));
            _EnumCodes.Add(Index.FTSE, Code.newInstance("^FTSE"));
            _EnumCodes.Add(Index.FCHI, Code.newInstance("^FCHI"));
            _EnumCodes.Add(Index.BSESN, Code.newInstance("^BSESN"));
            _EnumCodes.Add(Index.NSEI, Code.newInstance("^NSEI"));
            _EnumCodes.Add(Index.AORD, Code.newInstance("^AORD"));
            _EnumCodes.Add(Index.ATX, Code.newInstance("^ATX"));
            _EnumCodes.Add(Index.BFX, Code.newInstance("^BFX"));
            _EnumCodes.Add(Index.GSPTSE, Code.newInstance("^GSPTSE"));
            _EnumCodes.Add(Index.HSI, Code.newInstance("^HSI"));
            _EnumCodes.Add(Index.JKSE, Code.newInstance("^JKSE"));
            _EnumCodes.Add(Index.KS11, Code.newInstance("^KS11"));
            _EnumCodes.Add(Index.AEX, Code.newInstance("^AEX"));
            _EnumCodes.Add(Index.PSI20, Code.newInstance("^PSI20"));
            _EnumCodes.Add(Index.TWII, Code.newInstance("^TWII"));
            _EnumCodes.Add(Index.SSMI, Code.newInstance("^SSMI"));
            _EnumCodes.Add(Index.BVSP, Code.newInstance("^BVSP"));
            _EnumCodes.Add(Index.SSEC, Code.newInstance("000001.SS"));
        }

        public string GetIndexName(Index idx)
        {
            if (_EnumNames.ContainsKey(idx))
            {
                return _EnumNames[idx];
            }
            else
            {
                return "";
            }
        }

        public Code GetIndexCode(Index idx)
        {
            if (_EnumCodes.ContainsKey(idx))
            {
                return _EnumCodes[idx];
            }
            else
            {
                return null;
            }
        }

        public static IndexHelper Instance()
        {
            return _Instance;
        }

        static IndexHelper _Instance = new IndexHelper();

        Dictionary<Index, string> _EnumNames = new Dictionary<Index, string>();
        Dictionary<Index, Code> _EnumCodes = new Dictionary<Index, Code>();
    }
}
