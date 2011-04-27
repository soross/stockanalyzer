StrategyBamboo： 竹节投资法，涨10%则卖出10%，跌10%则买进10% 
StrategyBear： 指令为如果没有股票，当日开盘买入，否则涨跌幅达到x%卖出（止盈止损策略）
StrategyKD： KDJ指标的低位K上穿D线和高位K下穿D线判断 KdMin = 25, KdMax = 75
StrategyMinMax： 最简单的算法，指令为前一日最低值买入，最高值卖出
StrategyPercent： 指令为前一日最低值买入，赚x%卖出
StrategyPeriodicity：定期买入
StrategyThreeDay: 给定三天，使用相应的判断条件生成交易策略。（如连续三天上涨，连续三天下跌等）
