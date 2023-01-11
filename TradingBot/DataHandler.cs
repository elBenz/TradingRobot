using System.Collections.Concurrent;

namespace TradingRobot
{
    class Data
    {
        public int reqId;
    }

    class TickData : Data
    {
        public TickData(int reqIdValue, int fieldValue, double tickValue)
        {
            value = tickValue;
            field = fieldValue;
            reqId = reqIdValue;
        }

        public double value;
        public int field;
    }

    class HistoricalData : Data
    {
        public HistoricalData(int reqIdValue, string timeValue, double openValue, double closeValue, double highValue, double lowValue, decimal volumeValue, int countValue, decimal wapValue)
        {
            reqId = reqIdValue;
            time = timeValue;
            open = openValue;
            close = closeValue;
            high = highValue;
            low = lowValue;
            volume = volumeValue;
            count = countValue;
            wap = wapValue;
        }
        public string time;
        public double open;
        public double close;
        public double high;
        public double low;
        public decimal volume;
        public int count;
        public decimal wap;
    }

    class EndOfData : Data
    {
        public EndOfData(int reqIdValue)
        {
            reqId = reqIdValue;
        }
    }

    static class DataHandler
    {
        static public ConcurrentBag<Data> data = new();
        static public void clearData()
        {
            data = new();
        }

        // If there is a object with the type EndOfData with the same reqId in the data, remove it and return all Data objects with the same reqId.
        // Then clear the data with the same reqId.
        static public Data[]? getData(int reqId)
        {
            if (data.Any(x => x.reqId == reqId && x.GetType() == typeof(EndOfData)))
            {
                Data[] dataToReturn = data.Where(x => x.reqId == reqId && x.GetType() != typeof(EndOfData)).ToArray();
                data = new(data.Where(x => x.reqId != reqId));
                return dataToReturn;
            }
            else
            {
                return null;
            }
        }
    }
}