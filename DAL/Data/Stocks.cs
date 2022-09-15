using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyStockApi.Data
{
    public class Stocks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string SymbolName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
