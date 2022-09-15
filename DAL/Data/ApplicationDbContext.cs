
using Microsoft.EntityFrameworkCore;

namespace CompanyStockApi.Data
{
    public class ApplicationDbContext:DbContext
    {

     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {

      }
        
        public DbSet<Stocks> Stocks { get; set; }
        public DbSet<MarketPriceDetails> MarketPriceDetails { get; set; }



    }
}
