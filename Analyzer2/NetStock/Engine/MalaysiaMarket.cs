using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class MalaysiaMarket : Market
    {
        /** Creates a new instance of MalaysiaMarket */
        public MalaysiaMarket(
                double mainBoardIndex, double mainBoardChange,
                double secondBoardIndex, double secondBoardChange,
                double mesdaqIndex, double mesdaqChange,
                int up, int down, int unchange, long volume,
                double value
                )
        {
            indexMap.Add(Index.KLSE, mainBoardIndex);
            indexMap.Add(Index.Second, secondBoardIndex);
            indexMap.Add(Index.Mesdaq, mesdaqIndex);

            changeMap.Add(Index.KLSE, mainBoardChange);
            changeMap.Add(Index.Second, secondBoardChange);
            changeMap.Add(Index.Mesdaq, mesdaqChange);

            numMap.Add(ChangeType.Up, up);
            numMap.Add(ChangeType.Down, down);
            numMap.Add(ChangeType.Unchange, unchange);

            this.volume = volume;
            this.value = value;
        }

        public double getIndex(Index index)
        {
            if (indexMap.ContainsKey(index))
            {
                return indexMap[index];
            }
            else
            {
                return 0.0;
            }
        }

        public double getChange(Index index)
        {
            if (changeMap.ContainsKey(index))
            {
                return changeMap[index];
            }
            else
            {
                return 0.0;
            }
        }

        public int getNumOfStockChange(ChangeType type)
        {
            if (numMap.ContainsKey(type))
            {
                return numMap[type];
            }
            else
            {
                return 0;
            }
        }

        public long getVolume()
        {
            return volume;
        }

        public double getValue()
        {
            return value;
        }

        private Dictionary<Index, Double> indexMap = new Dictionary<Index, Double>();
        private Dictionary<Index, Double> changeMap = new Dictionary<Index, Double>();
        private Dictionary<ChangeType, int> numMap = new Dictionary<ChangeType, int>();
        private long volume;
        private double value;


        public Country getCountry()
        {
            return Country.Malaysia;
        }
    }
}
