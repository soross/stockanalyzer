using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public class Factories
    {
        private Factories()
        {
            List<StockServerFactory> australiaList = new List<StockServerFactory>();
            List<StockServerFactory> austriaList = new List<StockServerFactory>();
            List<StockServerFactory> belgiumList = new List<StockServerFactory>();
            List<StockServerFactory> brazilList = new List<StockServerFactory>();
            List<StockServerFactory> canadaList = new List<StockServerFactory>();
            List<StockServerFactory> chinaList = new List<StockServerFactory>();
            List<StockServerFactory> denmarkList = new List<StockServerFactory>();
            List<StockServerFactory> franceList = new List<StockServerFactory>();
            List<StockServerFactory> germanyList = new List<StockServerFactory>();
            List<StockServerFactory> hongkongList = new List<StockServerFactory>();
            List<StockServerFactory> indiaList = new List<StockServerFactory>();
            List<StockServerFactory> indonesiaList = new List<StockServerFactory>();
            List<StockServerFactory> italyList = new List<StockServerFactory>();
            List<StockServerFactory> koreaList = new List<StockServerFactory>();
            List<StockServerFactory> malaysiaList = new List<StockServerFactory>();
            List<StockServerFactory> netherlandsList = new List<StockServerFactory>();
            List<StockServerFactory> norwayList = new List<StockServerFactory>();
            List<StockServerFactory> portugalList = new List<StockServerFactory>();
            List<StockServerFactory> singaporeList = new List<StockServerFactory>();
            List<StockServerFactory> spainList = new List<StockServerFactory>();
            List<StockServerFactory> swedenList = new List<StockServerFactory>();
            List<StockServerFactory> switzerlandList = new List<StockServerFactory>();
            List<StockServerFactory> taiwanList = new List<StockServerFactory>();
            List<StockServerFactory> unitedKingdomList = new List<StockServerFactory>();
            List<StockServerFactory> unitedStateList = new List<StockServerFactory>();

            australiaList.Add(YahooStockServerFactory.newInstance(Country.Australia));
            austriaList.Add(YahooStockServerFactory.newInstance(Country.Austria));
            belgiumList.Add(YahooStockServerFactory.newInstance(Country.Belgium));
            brazilList.Add(BrazilYahooStockServerFactory.newInstance(Country.Brazil));
            canadaList.Add(YahooStockServerFactory.newInstance(Country.Canada));
            chinaList.Add(YahooStockServerFactory.newInstance(Country.China));
            denmarkList.Add(YahooStockServerFactory.newInstance(Country.Denmark));
            franceList.Add(YahooStockServerFactory.newInstance(Country.France));
            germanyList.Add(YahooStockServerFactory.newInstance(Country.Germany));
            hongkongList.Add(SingaporeYahooStockServerFactory.newInstance(Country.HongKong));
            indiaList.Add(YahooStockServerFactory.newInstance(Country.India));
            indonesiaList.Add(SingaporeYahooStockServerFactory.newInstance(Country.Indonesia));
            italyList.Add(YahooStockServerFactory.newInstance(Country.Italy));
            koreaList.Add(SingaporeYahooStockServerFactory.newInstance(Country.Korea));
            malaysiaList.Add(SingaporeYahooStockServerFactory.newInstance(Country.Malaysia));
            netherlandsList.Add(YahooStockServerFactory.newInstance(Country.Netherlands));
            norwayList.Add(YahooStockServerFactory.newInstance(Country.Norway));
            portugalList.Add(YahooStockServerFactory.newInstance(Country.Portugal));
            singaporeList.Add(SingaporeYahooStockServerFactory.newInstance(Country.Singapore));
            spainList.Add(YahooStockServerFactory.newInstance(Country.Spain));
            swedenList.Add(YahooStockServerFactory.newInstance(Country.Sweden));
            switzerlandList.Add(YahooStockServerFactory.newInstance(Country.Switzerland));
            taiwanList.Add(SingaporeYahooStockServerFactory.newInstance(Country.Taiwan));
            unitedKingdomList.Add(YahooStockServerFactory.newInstance(Country.UnitedKingdom));
            unitedStateList.Add(YahooStockServerFactory.newInstance(Country.UnitedState));
            unitedStateList.Add(GoogleStockServerFactory.newInstance(Country.UnitedState));

            _map.Add(Country.Australia, australiaList);
            _map.Add(Country.Austria, austriaList);
            _map.Add(Country.Belgium, belgiumList);
            _map.Add(Country.Brazil, brazilList);
            _map.Add(Country.Canada, canadaList);
            _map.Add(Country.China, chinaList);
            _map.Add(Country.Denmark, denmarkList);
            _map.Add(Country.France, franceList);
            _map.Add(Country.Germany, germanyList);
            _map.Add(Country.HongKong, hongkongList);
            _map.Add(Country.India, indiaList);
            _map.Add(Country.Indonesia, indonesiaList);
            _map.Add(Country.Italy, italyList);
            _map.Add(Country.Korea, koreaList);
            _map.Add(Country.Malaysia, malaysiaList);
            _map.Add(Country.Netherlands, netherlandsList);
            _map.Add(Country.Norway, norwayList);
            _map.Add(Country.Portugal, portugalList);
            _map.Add(Country.Singapore, singaporeList);
            _map.Add(Country.Spain, spainList);
            _map.Add(Country.Sweden, swedenList);
            _map.Add(Country.Switzerland, switzerlandList);
            _map.Add(Country.Taiwan, taiwanList);
            _map.Add(Country.UnitedKingdom, unitedKingdomList);
            _map.Add(Country.UnitedState, unitedStateList);
        }

        public static Factories GetInstance()
        {
            return _Instance;
        }

        public List<StockServerFactory> getStockServerFactories(Country country)
        {
            if (_map.ContainsKey(country))
            {
                List<StockServerFactory> list = _map[country];
                return list;
            }
            else
            {
                return new List<StockServerFactory>();
            }
        }

        public void updatePrimaryStockServerFactory(Country country, StockServerFactory factory)
        {
            if (_map.ContainsKey(country))
            {
                List<StockServerFactory> stockServerFactories = _map[country];

                int index = 0;
                foreach (StockServerFactory stockServerFactory in stockServerFactories)
                {
                    if (stockServerFactory.GetType() == factory.GetType())
                    {
                        StockServerFactory tmp = stockServerFactories[0];
                        stockServerFactories[0] = stockServerFactory;
                        stockServerFactories[index] = tmp;
                        break;
                    }
                    index++;
                }
            }
        }

        static Factories _Instance = new Factories();

        private static Dictionary<Country, List<StockServerFactory>> _map = new Dictionary<Country, List<StockServerFactory>>();
    }
}
