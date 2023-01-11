namespace TradingRobot
{
    class StrategyHandler
    {
        private List<Task> strategies;

        // Run all tasks via a factory.
        public StrategyHandler()
        {
            strategies = new();
        }
        public void runStrategies()
        {
            if (strategies == null)
            {
                Console.WriteLine("No strategies added to the handler.");
                return;
            }
            Console.WriteLine("Running strategies...");
            // Start all strategies.
            strategies.ForEach(strategy => strategy.Start());

            // Wait for all tasks to complete.
            Task.WaitAll(strategies.ToArray());
            Console.WriteLine("All strategies completed.");

            // Disconnect from the API.
            APIHandler.disconnect();
        }

        // Add Strategy to the handler.
        public void addStrategy(Task strategy)
        {
            strategies.Add(strategy);
        }
    }
}