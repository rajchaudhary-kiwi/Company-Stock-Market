using System;

namespace CompanyStockApi.Models
{
    public class MarketPriceRequeestModel
    {
       // public int SymbolId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int interevaltime { get; set; }

    }
}
