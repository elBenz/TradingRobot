using System.Diagnostics;
using IBApi;

namespace TradingRobot
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (!APIHandler.connected) { }
            StrategyHandler strategyHandler = new();
            Strategy.Regression strategy = new();
            strategyHandler.addStrategy(strategy.task);
            strategyHandler.runStrategies();
        }
    }
}