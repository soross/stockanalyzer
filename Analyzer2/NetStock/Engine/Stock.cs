using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public class Stock
    {
        // Builder pattern.
        // We are using a builder pattern when face with many constructor 
        // parameters.
        public class Builder
        {
            // Required parameters.
            public Code _code;
            public Symbol _symbol;

            // Optional parameters - initialized to default values.
            public String _name = "";
            public Board _board = Board.Unknown;
            public Industry _industry = Industry.Unknown;
            public double _prevPrice = 0.0;
            public double _openPrice = 0.0;
            public double _lastPrice = 0.0;
            public double _highPrice = 0.0;
            public double _lowPrice = 0.0;
            public long _volume = 0;
            public double _changePrice = 0.0;
            public double _changePricePercentage = 0.0;
            public int _lastVolume = 0;
            public double _buyPrice = 0.0;
            public int _buyQuantity = 0;
            public double _sellPrice = 0.0;
            public int _sellQuantity = 0;
            public double _secondBuyPrice = 0.0;
            public int _secondBuyQuantity = 0;
            public double _secondSellPrice = 0.0;
            public int _secondSellQuantity = 0;
            public double _thirdBuyPrice = 0.0;
            public int _thirdBuyQuantity = 0;
            public double _thirdSellPrice = 0.0;
            public int _thirdSellQuantity = 0;
            // We suppose to provide a default value for calendar. However, it may
            // seem expensive. We will do it later during build.
            public SimpleDate _calendar = new SimpleDate();
            public bool _hasCalendarInitialized = false;

            public Builder(Code code, Symbol symbol)
            {
                this._code = code;
                this._symbol = symbol;
            }

            /**
             * @param name the name to set
             */
            public Builder name(String name)
            {
                this._name = name;
                return this;
            }

            /**
             * @param board the board to set
             */
            public Builder board(Board board)
            {
                this._board = board;
                return this;
            }

            /**
             * @param industry the industry to set
             */
            public Builder industry(Industry industry)
            {
                this._industry = industry;
                return this;
            }

            /**
             * @param prevPrice the prevPrice to set
             */
            public Builder prevPrice(double prevPrice)
            {
                this._prevPrice = prevPrice;
                return this;
            }

            /**
             * @param openPrice the openPrice to set
             */
            public Builder openPrice(double openPrice)
            {
                this._openPrice = openPrice;
                return this;
            }

            /**
             * @param lastPrice the lastPrice to set
             */
            public Builder lastPrice(double lastPrice)
            {
                this._lastPrice = lastPrice;
                return this;
            }

            /**
             * @param highPrice the highPrice to set
             */
            public Builder highPrice(double highPrice)
            {
                this._highPrice = highPrice;
                return this;
            }

            /**
             * @param lowPrice the lowPrice to set
             */
            public Builder lowPrice(double lowPrice)
            {
                this._lowPrice = lowPrice;
                return this;
            }

            /**
             * @param volume the volume to set
             */
            public Builder volume(long volume)
            {
                this._volume = volume;
                return this;
            }

            /**
             * @param changePrice the changePrice to set
             */
            public Builder changePrice(double changePrice)
            {
                this._changePrice = changePrice;
                return this;
            }

            /**
             * @param changePricePercentage the changePricePercentage to set
             */
            public Builder changePricePercentage(double changePricePercentage)
            {
                this._changePricePercentage = changePricePercentage;
                return this;
            }

            /**
             * @param lastVolume the lastVolume to set
             */
            public Builder lastVolume(int lastVolume)
            {
                this._lastVolume = lastVolume;
                return this;
            }

            /**
             * @param buyPrice the buyPrice to set
             */
            public Builder buyPrice(double buyPrice)
            {
                this._buyPrice = buyPrice;
                return this;
            }

            /**
             * @param buyQuantity the buyQuantity to set
             */
            public Builder buyQuantity(int buyQuantity)
            {
                this._buyQuantity = buyQuantity;
                return this;
            }

            /**
             * @param sellPrice the sellPrice to set
             */
            public Builder sellPrice(double sellPrice)
            {
                this._sellPrice = sellPrice;
                return this;
            }

            /**
             * @param sellQuantity the sellQuantity to set
             */
            public Builder sellQuantity(int sellQuantity)
            {
                this._sellQuantity = sellQuantity;
                return this;
            }

            /**
             * @param secondBuyPrice the secondBuyPrice to set
             */
            public Builder secondBuyPrice(double secondBuyPrice)
            {
                this._secondBuyPrice = secondBuyPrice;
                return this;
            }

            /**
             * @param secondBuyQuantity the secondBuyQuantity to set
             */
            public Builder secondBuyQuantity(int secondBuyQuantity)
            {
                this._secondBuyQuantity = secondBuyQuantity;
                return this;
            }

            /**
             * @param secondSellPrice the secondSellPrice to set
             */
            public Builder secondSellPrice(double secondSellPrice)
            {
                this._secondSellPrice = secondSellPrice;
                return this;
            }

            /**
             * @param secondSellQuantity the secondSellQuantity to set
             */
            public Builder secondSellQuantity(int secondSellQuantity)
            {
                this._secondSellQuantity = secondSellQuantity;
                return this;
            }

            /**
             * @param thirdBuyPrice the thirdBuyPrice to set
             */
            public Builder thirdBuyPrice(double thirdBuyPrice)
            {
                this._thirdBuyPrice = thirdBuyPrice;
                return this;
            }

            /**
             * @param thirdBuyQuantity the thirdBuyQuantity to set
             */
            public Builder thirdBuyQuantity(int thirdBuyQuantity)
            {
                this._thirdBuyQuantity = thirdBuyQuantity;
                return this;
            }

            /**
             * @param thirdSellPrice the thirdSellPrice to set
             */
            public Builder thirdSellPrice(double thirdSellPrice)
            {
                this._thirdSellPrice = thirdSellPrice;
                return this;
            }

            /**
             * @param thirdSellQuantity the thirdSellQuantity to set
             */
            public Builder thirdSellQuantity(int thirdSellQuantity)
            {
                this._thirdSellQuantity = thirdSellQuantity;
                return this;
            }

            /**
             * @param calendar the calendar to set
             */
            public Builder calendar(SimpleDate calendar)
            {
                this._calendar = calendar;
                this._hasCalendarInitialized = true;
                return this;
            }

            public Stock build()
            {
                if (_hasCalendarInitialized == false)
                {
                    // If we haven't initialized calendar before, do it right now.
                    this._calendar = new SimpleDate();
                }
                return new Stock(this);
            }
        }

        #region Constructors

        /** Creates a new instance of Stock */
        private Stock(Builder builder)
            : this(
                builder._code,
                builder._symbol,
                builder._name,
                builder._board,
                builder._industry,
                builder._prevPrice,
                builder._openPrice,
                builder._lastPrice,
                builder._highPrice,
                builder._lowPrice,
                builder._volume,
                builder._changePrice,
                builder._changePricePercentage,
                builder._lastVolume,
                builder._buyPrice,
                builder._buyQuantity,
                builder._sellPrice,
                builder._sellQuantity,
                builder._secondBuyPrice,
                builder._secondBuyQuantity,
                builder._secondSellPrice,
                builder._secondSellQuantity,
                builder._thirdBuyPrice,
                builder._thirdBuyQuantity,
                builder._thirdSellPrice,
                builder._thirdSellQuantity,
                builder._calendar
                )
        {
        }

        public Stock(
            Code code,
            Symbol symbol,
            String name,
            Board board,
            Industry industry,
            double prevPrice,
            double openPrice,
            double lastPrice,
            double highPrice,
            double lowPrice,
            // TODO: CRITICAL LONG BUG REVISED NEEDED.
            long volume,
            double changePrice,
            double changePricePercentage,
            int lastVolume,
            double buyPrice,
            int buyQuantity,
            double sellPrice,
            int sellQuantity,
            double secondBuyPrice,
            int secondBuyQuantity,
            double secondSellPrice,
            int secondSellQuantity,
            double thirdBuyPrice,
            int thirdBuyQuantity,
            double thirdSellPrice,
            int thirdSellQuantity,
            SimpleDate calendar
                    )
        {
            this.code = code;
            this.symbol = symbol;
            this.name = name;
            this.board = board;
            this.industry = industry;
            this.prevPrice = prevPrice;
            this.openPrice = openPrice;
            this.lastPrice = lastPrice;
            this.highPrice = highPrice;
            this.lowPrice = lowPrice;
            this.volume = volume;
            this.changePrice = changePrice;
            this.changePricePercentage = changePricePercentage;
            this.lastVolume = lastVolume;
            this.buyPrice = buyPrice;
            this.buyQuantity = buyQuantity;
            this.sellPrice = sellPrice;
            this.sellQuantity = sellQuantity;
            this.secondBuyPrice = secondBuyPrice;
            this.secondBuyQuantity = secondBuyQuantity;
            this.secondSellPrice = secondSellPrice;
            this.secondSellQuantity = secondSellQuantity;
            this.thirdBuyPrice = thirdBuyPrice;
            this.thirdBuyQuantity = thirdBuyQuantity;
            this.thirdSellPrice = thirdSellPrice;
            this.thirdSellQuantity = thirdSellQuantity;
            this.calendar = calendar;
        }

        // I didn't make this construcotr private. As I would like to make user able
        // to construct Stock either through this constructor or Builder.
        public Stock(Stock stock)
        {
            this.code = stock.code;
            this.symbol = stock.symbol;
            this.name = stock.name;
            this.board = stock.board;
            this.industry = stock.industry;
            this.prevPrice = stock.prevPrice;
            this.openPrice = stock.openPrice;
            this.lastPrice = stock.lastPrice;
            this.highPrice = stock.highPrice;
            this.lowPrice = stock.lowPrice;
            this.volume = stock.volume;
            this.changePrice = stock.changePrice;
            this.changePricePercentage = stock.changePricePercentage;
            this.lastVolume = stock.lastVolume;
            this.buyPrice = stock.buyPrice;
            this.buyQuantity = stock.buyQuantity;
            this.sellPrice = stock.sellPrice;
            this.sellQuantity = stock.sellQuantity;
            this.secondBuyPrice = stock.secondBuyPrice;
            this.secondBuyQuantity = stock.secondBuyQuantity;
            this.secondSellPrice = stock.secondSellPrice;
            this.secondSellQuantity = stock.secondSellQuantity;
            this.thirdBuyPrice = stock.thirdBuyPrice;
            this.thirdBuyQuantity = stock.thirdBuyQuantity;
            this.thirdSellPrice = stock.thirdSellPrice;
            this.thirdSellQuantity = stock.thirdSellQuantity;
            this.calendar = stock.calendar;
        }

        #endregion

        public Code getCode()
        {
            return code;
        }

        public Symbol getSymbol()
        {
            return symbol;
        }

        public String getName()
        {
            return name;
        }

        public Board getBoard()
        {
            return board;
        }

        public Industry getIndustry()
        {
            return industry;
        }

        public double getPrevPrice()
        {
            return prevPrice;
        }

        public double getOpenPrice()
        {
            return openPrice;
        }

        public double getLastPrice()
        {
            return lastPrice;
        }

        public double getHighPrice()
        {
            return highPrice;
        }

        public double getLowPrice()
        {
            return lowPrice;
        }

        // TODO: CRITICAL LONG BUG REVISED NEEDED.
        public long getVolume()
        {
            return volume;
        }

        public double getChangePrice()
        {
            return changePrice;
        }

        public double getChangePricePercentage()
        {
            return changePricePercentage;
        }

        public int getLastVolume()
        {
            return lastVolume;
        }

        public double getBuyPrice()
        {
            return buyPrice;
        }

        public int getBuyQuantity()
        {
            return buyQuantity;
        }

        public double getSellPrice()
        {
            return sellPrice;
        }

        public int getSellQuantity()
        {
            return sellQuantity;
        }

        public double getSecondBuyPrice()
        {
            return secondBuyPrice;
        }

        public int getSecondBuyQuantity()
        {
            return secondBuyQuantity;
        }

        public double getSecondSellPrice()
        {
            return secondSellPrice;
        }

        public int getSecondSellQuantity()
        {
            return secondSellQuantity;
        }

        public double getThirdBuyPrice()
        {
            return thirdBuyPrice;
        }

        public int getThirdBuyQuantity()
        {
            return thirdBuyQuantity;
        }

        public double getThirdSellPrice()
        {
            return thirdSellPrice;
        }

        public int getThirdSellQuantity()
        {
            return thirdSellQuantity;
        }

        public SimpleDate getCalendar()
        {
            return calendar;
        }

        /**
         * Returns a clone copy of this stock with its name being modified to
         * specified name. This stock remains unchanged, as it is designed as
         * immutable class.
         *
         * @param name Specified name to be modified to
         * @return A clone copy of this stock with its name being modified to
         * specified name.
         */
        public Stock deriveStock(String name)
        {
            return new Stock(
                this.code,
                this.symbol,
                name,
                this.board,
                this.industry,
                this.prevPrice,
                this.openPrice,
                this.lastPrice,
                this.highPrice,
                this.lowPrice,
                // TODO: CRITICAL LONG BUG REVISED NEEDED.
                this.volume,
                this.changePrice,
                this.changePricePercentage,
                this.lastVolume,
                this.buyPrice,
                this.buyQuantity,
                this.sellPrice,
                this.sellQuantity,
                this.secondBuyPrice,
                this.secondBuyQuantity,
                this.secondSellPrice,
                this.secondSellQuantity,
                this.thirdBuyPrice,
                this.thirdBuyQuantity,
                this.thirdSellPrice,
                this.thirdSellQuantity,
                this.calendar
            );
        }

        /**
         * Returns a clone copy of this stock with its symbol being modified to
         * specified symbol. This stock remains unchanged, as it is designed as
         * immutable class.
         *
         * @param symbol Specified symbol to be modified to
         * @return A clone copy of this stock with its symbol being modified to
         * specified symbol.
         */
        public Stock deriveStock(Symbol symbol)
        {
            return new Stock(
                this.code,
                symbol,
                this.name,
                this.board,
                this.industry,
                this.prevPrice,
                this.openPrice,
                this.lastPrice,
                this.highPrice,
                this.lowPrice,
                // TODO: CRITICAL LONG BUG REVISED NEEDED.
                this.volume,
                this.changePrice,
                this.changePricePercentage,
                this.lastVolume,
                this.buyPrice,
                this.buyQuantity,
                this.sellPrice,
                this.sellQuantity,
                this.secondBuyPrice,
                this.secondBuyQuantity,
                this.secondSellPrice,
                this.secondSellQuantity,
                this.thirdBuyPrice,
                this.thirdBuyQuantity,
                this.thirdSellPrice,
                this.thirdSellQuantity,
                this.calendar
            );
        }

        public String toString()
        {
            return "Stock" + "[code=" + code + ",symbol=" + symbol + ",name=" + name + ",board=" + board + ",industry=" + industry +
                    ",prevPrice=" + prevPrice + ",openPrice=" + openPrice + ",lastPrice=" + lastPrice + ",highPrice=" + highPrice + ",lowPrice=" + lowPrice +
                    ",volume=" + volume + ",changePrice=" + changePrice + ",changePricePercentage=" + changePricePercentage + ",lastVolume=" + lastVolume +
                    ",buyPrice=" + buyPrice + ",buyQuantity=" + buyQuantity + ",sellPrice=" + sellPrice + ",sellQuantity" + sellQuantity +
                    ",secondBuyPrice=" + secondBuyPrice + ",secondBuyQuantity=" + secondBuyQuantity + ",secondSellPrice=" + secondSellPrice + ",secondSellQuantity" + secondSellQuantity +
                    ",thirdBuyPrice=" + thirdBuyPrice + ",thirdBuyQuantity=" + thirdBuyQuantity + ",thirdSellPrice=" + thirdSellPrice + ",thirdSellQuantity" + thirdSellQuantity +
                    ",calendar=" + calendar + "]"
                    ;
        }

        private Code code;
        private Symbol symbol;
        private String name;
        private Board board;
        private Industry industry;
        private double prevPrice;
        private double openPrice;
        private double lastPrice;
        private double highPrice;
        private double lowPrice;
        // TODO: CRITICAL LONG BUG REVISED NEEDED.
        private long volume;
        private double changePrice;
        private double changePricePercentage;
        private int lastVolume;
        private double buyPrice;
        private int buyQuantity;
        private double sellPrice;
        private int sellQuantity;
        private double secondBuyPrice;
        private int secondBuyQuantity;
        private double secondSellPrice;
        private int secondSellQuantity;
        private double thirdBuyPrice;
        private int thirdBuyQuantity;
        private double thirdSellPrice;
        private int thirdSellQuantity;
        private SimpleDate calendar;

        public enum Board
        {
            // The following are naming conventions from CIMB :
            Main,                     // Main
            Second,                 // 2nd
            Mesdaq,                       // MESDAQ
            CallWarrant,            // ??
            KualaLumpur,
            SES,                             // Singapore
            Copenhagen,               // Denmark        
            Paris,                         // France
            Xetra,                         // Germany
            XETRA,
            Munich,
            Stuttgart,
            Berlin,
            Hamburg,
            Dusseldorf,
            Frankfurt,
            Hannover,
            Milan,                         // Italy
            Oslo,                           // Norway
            Madrid,                       // Spain
            MCE,
            MercadoContinuo,
            Stockholm,                 // Sweden
            FSI,                             // UK
            London,
            NasdaqSC,                   // US
            DJI,
            NasdaqNM,
            NYSE,
            Nasdaq,
            AMEX,
            PinkSheet,
            Sydney,                       // Australia
            ASX,
            Vienna,                       // Austria
            Brussels,                   // Belgium
            Toronto,                     // Canada
            HKSE,                           // Hong Kong
            Jakarta,                     // Indonesia
            KSE,                             // Korea
            KOSDAQ,
            Amsterdam,                 // Netherlands
            ENX,                             // Portugal
            Lisbon,
            VTX,                             // Switzerland
            Virt_X,
            Switzerland,
            Taiwan,                       // Taiwan
            Bombay,                       // India
            NSE,
            UserDefined,
            Unknown
        };

        public enum Industry
        {
            // The following are naming conventions from CIMB :
            ConsumerProducts,      // CONSUMER
            IndustrialProducts,  // IND-PROD
            Construction,               // CONSTRUCTN
            TradingServices,      // TRAD/SERV
            Technology,                   // TECHNOLOGY
            Infrastructure,           // IPC
            Finance,                         // FINANCE
            Hotels,                           // HOTELS
            Properties,                   // PROPERTIES 
            Plantation,                   // PLANTATION
            Mining,                           // MINING
            Trusts,                           // REITS
            CloseEndFund,             // CLOSED/FUND 
            ETF,                                 // ETF
            Loans,                             // LOANS
            CallWarrant,                // CALL-WARR
            UserDefined,
            Unknown
        };
    }
}
