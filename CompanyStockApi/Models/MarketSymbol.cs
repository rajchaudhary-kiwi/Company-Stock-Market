using CompanyStockApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CompanyStockApi.Models
{
    public class MarketSymbol
    {
        public int ID { get; set; }
        public int StocksMarketID { get; set; }
        public string Symbol { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
