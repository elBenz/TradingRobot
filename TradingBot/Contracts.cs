using IBApi;
namespace TradingRobot
{
    static class Contracts
    {
        // INTEREST RATES: 2Y NOTE, 5Y TREASURY BOND, 30Y TREASURY BOND
        public static Contract ZT = new()
        {
            ConId = 570640399,
            Exchange = "CBOT"
        };
        public static Contract ZF = new()
        {
            ConId = 570640394,
            Exchange = "CBOT"
        };
        public static Contract ZB = new()
        {
            ConId = 568735919,
            Exchange = "CBOT"
        };

        // FUTURES:
        public static Contract LBR = new()
        {
            ConId = 578106878,
            Exchange = "CME"
        };

        // OIL: CL1, Light Sweet Crude Oil
        public static Contract CL1 = new()
        {
            ConId = 296574745,
            Exchange = "NYMEX"
        };

        // EQUITIES: CIM, DRN, PLD, AMT, EQIX
        public static Contract CIM = new()
        {
            ConId = 187296614,
            Exchange = "SMART"
        };
        public static Contract DRN = new()
        {
            ConId = 61760197,
            Exchange = "SMART"
        };
        public static Contract PLD = new()
        {
            ConId = 88819288,
            Exchange = "SMART"
        };
        public static Contract AMT = new()
        {
            ConId = 99831145,
            Exchange = "SMART"
        };
        public static Contract EQIX = new()
        {
            ConId = 181764593,
            Exchange = "SMART"
        };

        // Real Estate ETFs
        public static Contract FRESX = new()
        {
            ConId = 141175539,
            Exchange = "FUNDSERV"
        };
        public static Contract PIREX = new()
        {
            ConId = 92085032,
            Exchange = "FUNDSERV"
        };
        public static Contract FSRNX = new()
        {
            ConId = 141168055,
            Exchange = "FUNDSERV"
        };
        public static Contract TIREX = new()
        {
            ConId = 141455006,
            Exchange = "FUNDSERV"
        };
        public static Contract POSIX = new()
        {
            ConId = 141426838,
            Exchange = "FUNDSERV"
        };
        public static Contract NGREX = new()
        {
            ConId = 141176367,
            Exchange = "FUNDSERV"
        };
        public static Contract BREIX = new()
        {
            ConId = 121985181,
            Exchange = "FUNDSERV"
        };
        public static Contract DJUSRE = new()
        {
            ConId = 551601510,
            Exchange = "CBOT"
        };
        public static Contract IXRE = new()
        {
            ConId = 533620653,
            Exchange = "CME"
        };

        // XOM, HES, PXD, TRGP, SLB, HAL, MPC (CrudeOil Related Stocks)
        public static Contract XOM = new()
        {
            ConId = 13977,
            Exchange = "NYSE"
        };
        public static Contract HES = new()
        {
            ConId = 39118796,
            Exchange = "NYSE"
        };
        public static Contract PXD = new()
        {
            ConId = 3142097,
            Exchange = "NYSE"
        };
        public static Contract TRGP = new()
        {
            ConId = 81671838,
            Exchange = "NYSE"
        };
        public static Contract SLB = new()
        {
            ConId = 12200,
            Exchange = "NYSE"
        };
        public static Contract HAL = new()
        {
            ConId = 7890,
            Exchange = "NYSE"
        };
        public static Contract MPC = new()
        {
            ConId = 89495776,
            Exchange = "NYSE"
        };

        // DJIA, SP500
        public static Contract YMMAR23 = new()
        {
            ConId = 551601503,
            Exchange = "CBOT"
        };
        public static Contract ESH3 = new()
        {
            ConId = 495512572,
            Exchange = "CME"
        };

        //MSFT, AAPL, TSLA, BTC, ETH, GOLD, WTI
        public static Contract MSFT = new()
        {
            ConId = 272093,
            Exchange = "SMART"
        };
        public static Contract AAPL = new()
        {
            ConId = 265598,
            Exchange = "SMART"
        };
        public static Contract TSLA = new()
        {
            ConId = 76792991,
            Exchange = "SMART"
        };
        public static Contract BTC = new()
        {
            ConId = 479624278,
            Exchange = "PAXOS"
        };
        public static Contract ETH = new()
        {
            ConId = 495759171,
            Exchange = "PAXOS"
        };
        public static Contract USGOLD = new()
        {
            ConId = 474636065,
            Exchange = "IPMETAL"
        };
        public static Contract WTIH3 = new()
        {
            ConId = 297249704,
            Exchange = "IPE"
        };

        // AMZN, IBM, GOOG, LTC, MATIC, SILVER, WEAT
        public static Contract AMZN = new()
        {
            ConId = 3691937,
            Exchange = "SMART"
        };
        public static Contract IBM = new()
        {
            ConId = 8314,
            Exchange = "NYSE"
        };
        public static Contract GOOGL = new()
        {
            ConId = 208813719,
            Exchange = "SMART"
        };
        public static Contract LTC = new()
        {
            ConId = 498989715,
            Exchange = "PAXOS"
        };
        public static Contract MATIC = new()
        {
            ConId = 541416663,
            Exchange = "PAXOS"
        };
        public static Contract XAGUSD = new()
        {
            ConId = 77124483,
        };
        public static Contract ZWMAR23 = new()
        {
            ConId = 434140037,
            Exchange = "CBOT"
        };
    }
}