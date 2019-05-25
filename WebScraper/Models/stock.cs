namespace WebScraper.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class stock
    {
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string PercentChg { get; set; }
        public string Currency { get; set; }
        public string MarketTime { get; set; }
        public string Volume { get; set; }
        public string AvgVol { get; set; }
        public string MarketCap { get; set; }
    }
}
