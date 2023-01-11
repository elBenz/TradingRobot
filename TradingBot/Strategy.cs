using IBApi;

namespace TradingRobot
{
    static class Strategy
    {
        static string date = System.DateTime.Now.ToString("yyyyMMdd");

        public class DowJonesStrategy
        {
            // The Dow Jones Strategy Task has to be executed every 1 minute.
            public Task task = new Task(() =>
            {
                Console.WriteLine("Dow Jones Strategy started.");
                // Request the Dow Jones yesterday's close price.
                int reqId = APIHandler.reqHistData(Contracts.ESH3, date + "-00:00:00", "1 D", "1 day", "TRADES", 1, 1);

                // Wait for the data to be received.
                Data[]? data;
                while ((data = DataHandler.getData(reqId)) == null) { };

                // Write today's date with the Dow Jones yesterday's close price to the file.
                using StreamWriter sw = new("./DJIA.txt", true);
                sw.Write(date + "," + ((HistoricalData)data[0]).close);
                sw.Close();

                // Keep the Dow Jones Strategy Task running every 1 minute.
                AutoResetEvent autoEvent = new(false);
                Timer timer = new Timer(callbackStrategy, autoEvent, 0, 60000);
                autoEvent.WaitOne();
                timer.Dispose();
                Console.WriteLine("Dow Jones Strategy finished.");
            });

            public static void callbackStrategy(object stateInfo)
            {
                // Request the Dow Jones last price.
                int reqId = APIHandler.reqTickData(Contracts.ESH3, "", true);

                // Wait for the data to be received.
                Data[]? data;
                while ((data = DataHandler.getData(reqId)) == null) { };

                // Write the Dow Jones last price to the file.
                using (StreamWriter sw = new("./DJIA.txt", true))
                {
                    sw.Write("," + ((TickData)data[0]).value);
                }

                // Get the two last prices from the last line of the file and compare their value.
                string[] lines = File.ReadAllLines("DJIA.txt");
                string[] prices = lines[lines.Length - 1].Split(",");
                decimal ytdPrice = Convert.ToDecimal(prices[1]);
                decimal lastPrice = Convert.ToDecimal(prices[prices.Length - 1]);
                if (lastPrice > ytdPrice + 100 && (lastPrice != -1 && ytdPrice != -1))
                {
                    // If the last price is higher than the previous one, buy MSFT, AAPL, TSLA, BTC, ETH, GOLD, WTI.
                    // Then disconnect from the TWS API, close the file and exit the program.
                    APIHandler.placeMktOrder(Contracts.MSFT, "BUY", 100);
                    APIHandler.placeMktOrder(Contracts.AAPL, "BUY", 100);
                    APIHandler.placeMktOrder(Contracts.TSLA, "BUY", 100);
                    APIHandler.placeMktOrder(Contracts.BTC, "BUY", 100);
                    APIHandler.placeMktOrder(Contracts.ETH, "BUY", 100);
                    APIHandler.placeMktOrder(Contracts.USGOLD, "BUY", 100);
                    APIHandler.placeMktOrder(Contracts.WTIH3, "BUY", 100);

                    // Add carriage return to the file.
                    using (StreamWriter sw = new("./DJIA.txt", true))
                    {
                        sw.Write("\n");
                    }

                    // Tell waiting threads that the Dow Jones Strategy Task has finished.
                    ((AutoResetEvent)stateInfo).Set();
                }
            }

        }

        public class SP500Strategy
        {
            // The Dow Jones Strategy Task has to be executed every 1 minute.
            public Task task = new Task(() =>
            {
                Console.WriteLine("SP500 Strategy started.");
                // Request the Dow Jones yesterday's close price.
                int reqId = APIHandler.reqHistData(Contracts.YMMAR23, date + "-00:00:00", "1 D", "1 day", "TRADES", 1, 1);

                // Wait for the data to be received.
                Data[]? data;
                while ((data = DataHandler.getData(reqId)) == null) { };

                // Write today's date with the Dow Jones yesterday's close price to the file.
                using StreamWriter sw = new("./SPX.txt", true);
                sw.Write(date + "," + ((HistoricalData)data[0]).close);
                sw.Close();

                // Keep the SP500 Strategy Task running every 1 minute.
                AutoResetEvent autoEvent = new(false);
                Timer timer = new Timer(callbackStrategy, autoEvent, 0, 60000);
                autoEvent.WaitOne();
                timer.Dispose();
                Console.WriteLine("SP500 Strategy finished.");
            });

            public static void callbackStrategy(object stateInfo)
            {
                // Request the Dow Jones last price.
                int reqId = APIHandler.reqTickData(Contracts.YMMAR23, "", true);

                // Wait for the data to be received.
                Data[]? data;
                while ((data = DataHandler.getData(reqId)) == null) { };

                // Write the Dow Jones last price to the file.
                using (StreamWriter sw = new("./SPX.txt", true))
                {
                    sw.Write("," + ((TickData)data[0]).value);
                }

                // Get the two last prices from the last line of the file and compare their value.
                string[] lines = File.ReadAllLines("./SPX.txt");
                string[] prices = lines[lines.Length - 1].Split(",");
                decimal ytdPrice = Convert.ToDecimal(prices[1]);
                decimal lastPrice = Convert.ToDecimal(prices[prices.Length - 1]);
                if (lastPrice < ytdPrice - 5 && (lastPrice != -1 && ytdPrice != -1))
                {
                    // If the last price is higher than the previous one, sell AMZN, IBM, GOOG, LTC, MATIC, SILVER, WEAT
                    // Then close the file and end the strategy.
                    APIHandler.placeMktOrder(Contracts.AMZN, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.IBM, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.GOOGL, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.LTC, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.MATIC, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.XAGUSD, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.ZWMAR23, "SELL", 100);

                    // Add carriage return to the file.
                    using (StreamWriter sw = new("./SPX.txt", true))
                    {
                        sw.Write("\n");
                    }

                    // Tell waiting threads that the SP500 Strategy Task has finished.
                    ((AutoResetEvent)stateInfo).Set();
                }
            }
        }

        public class CrudeOilStrategy
        {
            // Request the WTI last price.
            public Task task = new Task(() =>
        {
            Console.WriteLine("Crude Oil Strategy started.");
            // Request the Dow Jones yesterday's close price.
            int reqId = APIHandler.reqHistData(Contracts.WTIH3, date + "-00:00:00", "1 D", "1 day", "TRADES", 1, 1);

            // Wait for the data to be received.
            Data[]? data;
            while ((data = DataHandler.getData(reqId)) == null) { };

            // Write today's date with the WTI yesterday's close price to the file.
            using StreamWriter sw = new("./WTI.txt", true);
            sw.Write(date + "," + ((HistoricalData)data[0]).close);
            sw.Close();

            // Keep the Crude Oil Strategy Task running every 1 minute.
            AutoResetEvent autoEvent = new(false);
            Timer timer = new Timer(callbackStrategy, autoEvent, 0, 60000);
            autoEvent.WaitOne();
            timer.Dispose();
            Console.WriteLine("WTI Strategy finished.");
        });

            public static void callbackStrategy(object stateInfo)
            {
                // Request the Dow Jones last price.
                int reqId = APIHandler.reqTickData(Contracts.WTIH3, "", true);

                // Wait for the data to be received.
                Data[]? data;
                while ((data = DataHandler.getData(reqId)) == null) { };

                // Write the WTI last price to the file.
                using (StreamWriter sw = new("./WTI.txt", true))
                {
                    sw.Write("," + ((TickData)data[0]).value);
                }

                // Get the two last prices from the last line of the file and compare their value.
                string[] lines = File.ReadAllLines("./WTI.txt");
                string[] prices = lines[lines.Length - 1].Split(",");
                decimal ytdPrice = Convert.ToDecimal(prices[1]);
                decimal lastPrice = Convert.ToDecimal(prices[prices.Length - 1]);
                if (lastPrice > ytdPrice + 20 && (lastPrice != -1 && ytdPrice != -1))
                {
                    // If the last price is higher than the previous one, sell AMZN, IBM, GOOG, LTC, MATIC, SILVER, WEAT
                    // Then close the file and end the strategy.
                    APIHandler.placeMktOrder(Contracts.XOM, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.HES, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.PXD, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.TRGP, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.SLB, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.HAL, "SELL", 100);
                    APIHandler.placeMktOrder(Contracts.MPC, "SELL", 100);

                    // Add carriage return to the file.
                    using (StreamWriter sw = new("./WTI.txt", true))
                    {
                        sw.Write("\n");
                    }

                    // Tell waiting threads that the Crude Oil Strategy Task has finished.
                    ((AutoResetEvent)stateInfo).Set();
                }
            }
        }
        public class Regression
        {
            public Task task = new Task(() =>
        {
            Console.WriteLine("Regression started.");
            // List of contracts to analyze.
            Contract[] contracts = { Contracts.ZT, Contracts.ZF, Contracts.ZB, Contracts.LBR, Contracts.CL1, Contracts.CIM, Contracts.DRN, Contracts.PLD, Contracts.AMT, Contracts.EQIX, Contracts.DJUSRE, Contracts.IXRE };
            Parallel.ForEach(contracts, contract =>
            {
                runRegression(contract, "10 D");
                runRegression(contract, "30 D");
                runRegression(contract, "90 D");
                Console.WriteLine("Regression done for contract " + contract.ConId);
            });
            Console.WriteLine("Regression finished.");
        });

            private static double approxMovingAverage(double avg, double currentPrice, int N)
            {
                avg -= avg / N;
                avg += currentPrice / N;
                return avg;
            }

            private static void runRegression(Contract contract, string period)
            {
                // Request the historical data
                int reqId = APIHandler.reqHistData(contract, date + "-00:00:00", period, "10 mins", "TRADES", 1, 1);

                // Wait for the data to be received.
                Data[]? data;
                while ((data = DataHandler.getData(reqId)) == null) { };

                // Initialize variables
                string[] timestamp;
                double currentPrice;
                double previousPrice = 0;
                double changesLastPrice;
                double percentageChangesLastPrice;
                decimal previousVol = 0;
                double cumulativeAggrAvg;
                double previousCumulativeAggrAvg = 0;
                decimal previousCumulativeAggrVolAvg = 0;
                decimal changesVol;
                decimal currentVol;
                decimal cumulativeAggrVolAvg;
                decimal percentageChangesVol;
                double SMA5 = 0;
                double SMA20 = 0;
                double previousSMA5 = 0;
                double previousSMA20 = 0;
                int volCondition = 0;
                int SMACondition = 0;
                string order = "";
                int counter = 1;
                bool first = true;

                // Write in file every data received from last to first with time (should be between 9:30 and 16) and price.
                using StreamWriter sw = new(contract.ConId + "_" + period.Replace(" ", "") + ".txt", true);
                sw.Write("N-term, time, date, conId, Previous Price, Current Price, Difference in Price ($), Difference in Price (%), cumulativeAggregatedAvg, Previous Volume, Current Volume, Difference in Volume, cumulativeAggregatedAvgVolume, SMA 5, SMA20, Crossing SMA Condition, Volume Condition, Order\n");
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    timestamp = ((HistoricalData)data[i]).time.Split(" ");
                    if (timestamp[1].CompareTo("09:30:00") >= 0 && timestamp[1].CompareTo("16:00:00") < 0)
                    {
                        // if first time, ytdPrice and previous is the open price
                        if (first)
                        {
                            previousPrice = ((HistoricalData)data[i]).open;
                            previousVol = ((HistoricalData)data[i]).volume;
                            first = false;
                        }

                        // Set variables
                        currentVol = ((HistoricalData)data[i]).volume;
                        currentPrice = ((HistoricalData)data[i]).close;
                        changesLastPrice = currentPrice - previousPrice;
                        changesVol = currentVol - previousVol;
                        percentageChangesLastPrice = changesLastPrice / previousPrice;
                        percentageChangesVol = (previousVol != 0) ? changesVol / previousVol : 0;
                        cumulativeAggrVolAvg = previousCumulativeAggrVolAvg + ((Math.Abs(percentageChangesVol) - previousCumulativeAggrVolAvg) / counter);
                        cumulativeAggrAvg = previousCumulativeAggrAvg + ((Math.Abs(percentageChangesLastPrice) - previousCumulativeAggrAvg) / counter);
                        SMA5 = approxMovingAverage(SMA5, currentPrice, 5);
                        SMA20 = approxMovingAverage(SMA20, currentPrice, 20);

                        // Set conditions
                        // Volume Condition
                        volCondition = (percentageChangesVol > (previousCumulativeAggrVolAvg * 1.5M) ? 1 : 0);
                        // SMA Condition
                        if (counter > 20)
                        {
                            if (previousSMA5 < previousSMA20 && SMA5 > SMA20)
                            {
                                SMACondition = 1;
                            }
                            else if (previousSMA5 > previousSMA20 && SMA5 < SMA20)
                            {
                                SMACondition = -1;
                            }
                            else
                            {
                                SMACondition = 0;
                            }
                        }

                        // Order if conditions are met
                        if (volCondition == 1 && SMACondition == 1)
                        {
                            order = "BUY";
                        }
                        else if (volCondition == 1 && SMACondition == -1)
                        {
                            order = "SELL";
                        }
                        else
                        {
                            order = "";
                        }

                        // Write row to file
                        sw.Write(counter - 1 + "," + timestamp[1] + "," + timestamp[0] + "," + contract.ConId + "," + previousPrice + "," + currentPrice + "," + changesLastPrice + "," + percentageChangesLastPrice + "," + cumulativeAggrAvg + "," + previousVol + "," + currentVol + "," + changesVol + "," + cumulativeAggrVolAvg + "," + (counter >= 5 ? SMA5 : "") + "," + (counter >= 20 ? SMA20 : "") + "," + SMACondition + "," + volCondition + "," + order + "\n");

                        // If a buy or sell order is made, write it to a secondary file
                        using StreamWriter sw2 = new("orders.txt", true);
                        if (order != "")
                        {
                            sw2.Write(contract.ConId + "," + timestamp[1] + "," + timestamp[0] + "," + previousPrice + "," + currentPrice + "," + changesLastPrice + "," + percentageChangesLastPrice + "," + cumulativeAggrAvg + "," + previousVol + "," + currentVol + "," + changesVol + "," + cumulativeAggrVolAvg + "," + (counter >= 5 ? SMA5 : "") + "," + (counter >= 20 ? SMA20 : "") + "," + SMACondition + "," + volCondition + "," + order + "\n");
                        }

                        // Update variables
                        previousPrice = currentPrice;
                        previousVol = currentVol;
                        previousCumulativeAggrAvg = cumulativeAggrAvg;
                        previousCumulativeAggrVolAvg = cumulativeAggrVolAvg;
                        previousSMA5 = SMA5;
                        previousSMA20 = SMA20;
                        counter++;
                    }
                }
            }
        }
    }
}