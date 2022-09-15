using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class PriceVariationResponseModel
    {
        public double Price { get; set; }
        public string SymbolName { get; set; }
        public DateTime PriceUpdateDate { get; set; }
    }
}
