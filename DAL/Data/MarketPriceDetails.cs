using CompanyStockApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyStockApi.Data
{
    public class MarketPriceDetails
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("StocksMarketID")]
        public virtual Stocks Stock { get; set; }
        public int StocksMarketID { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
       
    }
}

