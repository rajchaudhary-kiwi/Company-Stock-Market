using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Threading;

namespace CompanyStockApi.Models
{
    public class StockModelAdd
    {

        
        [Required]

        public string CompanyName { get; set; }
        [Required]

        public string Symbol { get; set; }
        [Required]

        public double CurrentPrice { get; set; }

        public bool Active { get; set; }

        
    }
}
