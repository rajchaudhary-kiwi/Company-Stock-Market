using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service
{
    public class IStocksModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string SymbolName { get; set; }

        public string Symbol { get; set; }

        public double CurrentPrice { get; set; }

        public bool Active { get; set; }
    }
}
