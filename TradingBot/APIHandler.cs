using IBApi;

namespace TradingRobot
{
    class ActivePosition
    {
        public ActivePosition(Contract contract, Order order)
        {
            this.contract = contract;
            this.order = order;
        }
        public Contract contract;
        public Order order;
    }

    static class APIHandler
    {
        static private EWrapperImpl _wrapper = new();
        static private List<ActivePosition> _activePositions = new();
        static public bool connected = false;
        static public int reqHistData(Contract contract, string endDateTime, string duration, string barSize, string whatToShow, int useRTH, int formatDate)
        {
            int id = Interlocked.Increment(ref _wrapper.nextOrderId);
            _wrapper.ClientSocket.reqHistoricalData(id, contract, endDateTime, duration, barSize, whatToShow, useRTH, formatDate, false, null);
            return id;
        }
        static public int reqTickData(Contract contract, string genericTickList, bool snapshot)
        {
            int id = Interlocked.Increment(ref _wrapper.nextOrderId);
            _wrapper.ClientSocket.reqMktData(id, contract, genericTickList, snapshot, false, null);
            return id;
        }
        static public void placeMktOrder(Contract contract, string action, int quantity)
        {
            int id = Interlocked.Increment(ref _wrapper.nextOrderId);
            Order order = new();
            order.Action = action;
            order.TotalQuantity = quantity;
            order.OrderType = "MKT";
            _wrapper.ClientSocket.placeOrder(id, contract, order);
            _activePositions.Add(new ActivePosition(contract, order));
        }
        static public void closeAllPositions()
        {
            int id;
            foreach (ActivePosition position in _activePositions)
            {
                id = Interlocked.Increment(ref _wrapper.nextOrderId);
                position.order.Action = position.order.Action == "BUY" ? "SELL" : "BUY";
                position.order.TotalQuantity = position.order.TotalQuantity;
                position.order.OrderType = "MKT";
                _wrapper.ClientSocket.placeOrder(id, position.contract, position.order);
            }
        }
        static public void disconnect()
        {
            _wrapper.ClientSocket.eDisconnect();
        }
    }
}