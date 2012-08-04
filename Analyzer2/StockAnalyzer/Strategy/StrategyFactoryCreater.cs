using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Factory;

namespace FinanceAnalyzer.Strategy
{
    public enum StrategyFactoryType
    {
        Normal,
        Kd,
        RiseDown,
        Spike,
        Volume,
        Undefine
    };

    public class StrategyFactoryCreater
    {
        private StrategyFactoryCreater()
        {
        }

        public static StrategyFactoryCreater Instance()
        {
            return Creator_;
        }

        public IStrategyFactory CreateFactory(StrategyFactoryType tp)
        {
            switch (tp)
            {
                case StrategyFactoryType.Kd:
                    { 
                        KdAdjustFactory factory = new KdAdjustFactory();
                        factory.Init();

                        return factory;
                    }
                case StrategyFactoryType.Normal:
                    {
                        StrategyFactory factory = new StrategyFactory();
                        factory.Init();

                        return factory;
                    }
                case StrategyFactoryType.RiseDown:
                    {
                        RiseDownAdjustFactory factory = new RiseDownAdjustFactory();
                        factory.Init();

                        return factory;
                    }
                case StrategyFactoryType.Spike:
                    {
                        SpikeAdjustFactory factory = new SpikeAdjustFactory();
                        factory.Init();

                        return factory;
                    }
                case StrategyFactoryType.Volume:
                    {
                        VolumeAdjustFactory factory = new VolumeAdjustFactory();
                        factory.Init();

                        return factory;
                    }
                default:
                    return null;
            }
        }

        static StrategyFactoryCreater Creator_ = new StrategyFactoryCreater();
    }
}
