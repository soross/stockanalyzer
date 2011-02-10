using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DotNetStock.Engine
{
    class Utils
    {
        /**
     * Returns JSON string, by parsing respond from Google server.
     *
     * @param respond string returned from Google server directly
     * @return JSON string, by parsing respond from Google server
     */
        public static String GoogleRespondToJSON(String respond)
        {
            int beginIndex = respond.IndexOf("[");
            int endIndex = respond.LastIndexOf("]");
            if (beginIndex < 0)
            {
                return "";
            }
            if (beginIndex > endIndex)
            {
                return "";
            }
            return respond.Substring(beginIndex, endIndex + 1);
        }

        /**
         * Returns JSON string, by parsing respond from Yahoo server.
         *
         * @param respond string returned from Yahoo server directly
         * @return JSON string, by parsing respond from Yahoo server
         */
        public static String YahooRespondToJSON(String respond)
        {
            int beginIndex = respond.IndexOf("{");
            int endIndex = respond.LastIndexOf("}");
            if (beginIndex < 0)
            {
                return "";
            }
            if (beginIndex > endIndex)
            {
                return "";
            }
            return respond.Substring(beginIndex, endIndex + 1);
        }

        public static Code toYahooFormat(Code code, Country country)
        {
            if (code == null)
            {
                throw new ArgumentException("Method parameters cannot be null in toYahooFormat");
            }

            Code result = code;

            if (country == Country.Malaysia)
            {
                String _code = code.toString();
                // Index's code start with ^. We will not intrude index's code.
                if (_code.StartsWith("^") == false && _code.EndsWith(".KL") == false)
                {
                    // This is not index's code, and it does not end with .KL.
                    // Let's intrude it!
                    result = Code.newInstance(_code + ".KL");
                }
            }

            return result;
        }

        public static StockServer emptyStockServer()
        {
            return null;
        }

        public static List<Index> getStockIndices(Country country)
        {
            switch (country)
            {
                case Country.Australia:
                    return (Utils.australiaIndices);
                case Country.Austria:
                    return (Utils.austriaIndices);
                case Country.Belgium:
                    return (Utils.belgiumIndices);
                case Country.Brazil:
                    return (Utils.brazilIndices);
                case Country.Canada:
                    return (Utils.canadaIndices);
                case Country.China:
                    return (Utils.chinaIndices);
                case Country.Denmark:
                    return (Utils.denmarkIndices);
                case Country.France:
                    return (Utils.franceIndices);
                case Country.Germany:
                    return (Utils.germanyIndices);
                case Country.HongKong:
                    return (Utils.hongkongIndices);
                case Country.India:
                    return (Utils.indiaIndices);
                case Country.Indonesia:
                    return (Utils.indonesiaIndices);
                case Country.Italy:
                    return (Utils.italyIndices);
                case Country.Korea:
                    return (Utils.koreaIndices);
                case Country.Malaysia:
                    return (Utils.malaysiaIndices);
                case Country.Netherlands:
                    return (Utils.netherlandsIndices);
                case Country.Norway:
                    return (Utils.norwayIndices);
                case Country.Portugal:
                    return (Utils.portugalIndices);
                case Country.Singapore:
                    return (Utils.singaporeIndices);
                case Country.Spain:
                    return (Utils.spainIndices);
                case Country.Sweden:
                    return (Utils.swedenIndices);
                case Country.Switzerland:
                    return (Utils.switzerlandIndices);
                case Country.Taiwan:
                    return (Utils.taiwanIndices);
                case Country.UnitedKingdom:
                    return (Utils.unitedKingdomIndices);
                case Country.UnitedState:
                    return (Utils.unitedStateIndices);
            }

            return new List<Index>();
        }

        private static List<Index> australiaIndices = new List<Index>() { Index.AORD };
        private static List<Index> austriaIndices = new List<Index>() { Index.ATX };
        private static List<Index> belgiumIndices = new List<Index>() { Index.BFX };
        private static List<Index> brazilIndices = new List<Index>() { Index.BVSP };
        private static List<Index> canadaIndices = new List<Index>() { Index.GSPTSE };
        private static List<Index> chinaIndices = new List<Index>() { Index.SSEC };
        private static List<Index> denmarkIndices = new List<Index>() { Index.OMXC20CO };
        private static List<Index> franceIndices = new List<Index>() { Index.FCHI };
        private static List<Index> germanyIndices = new List<Index>() { Index.DAX };
        private static List<Index> hongkongIndices = new List<Index>() { Index.HSI };
        private static List<Index> indiaIndices = new List<Index>() { Index.BSESN, Index.NSEI };
        private static List<Index> indonesiaIndices = new List<Index>() { Index.JKSE };
        private static List<Index> italyIndices = new List<Index>() { Index.SPMIB };
        private static List<Index> koreaIndices = new List<Index>() { Index.KS11 };
        private static List<Index> malaysiaIndices = new List<Index>() { Index.KLSE, Index.Second, Index.Mesdaq };
        private static List<Index> netherlandsIndices = new List<Index>() { Index.AEX };
        private static List<Index> norwayIndices = new List<Index>() { Index.OSEAX };
        private static List<Index> portugalIndices = new List<Index>() { Index.PSI20 };
        private static List<Index> singaporeIndices = new List<Index>() { Index.STI };
        private static List<Index> spainIndices = new List<Index>() { Index.SMSI };
        private static List<Index> swedenIndices = new List<Index>() { Index.OMXSPI };
        private static List<Index> switzerlandIndices = new List<Index>() { Index.SSMI };
        private static List<Index> taiwanIndices = new List<Index>() { Index.TWII };
        private static List<Index> unitedKingdomIndices = new List<Index>() { Index.FTSE };
        private static List<Index> unitedStateIndices = new List<Index>() { Index.DJI, Index.IXIC };
    }
}
