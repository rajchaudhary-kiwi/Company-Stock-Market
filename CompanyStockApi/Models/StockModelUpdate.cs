using System.ComponentModel.DataAnnotations;

namespace CompanyStockApi.Models
{
    public class StockModelUpdate
    {
        public int Id { get; set; }
        [Required]

        public string CompanyName { get; set; }
        [Required]

        public string Symbol { get; set; }
        [Required]

        public double CurrentPrice { get; set; }

        public bool Active { get; set; }
    }
}
