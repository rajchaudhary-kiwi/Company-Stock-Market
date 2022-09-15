using CompanyStockApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarketPriceUpdateDoWorker
{
    public class Program
    {

        private static ApplicationDbContext _appDbContext;

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            string strCon = "Server=LAPTOP-FC8PBLOD\\SQLEXPRESS;Database=CompanyStockApi;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(strCon));

            var serviceProvider = services.BuildServiceProvider();
            _appDbContext = serviceProvider.GetService<ApplicationDbContext>();
            Utility utility = new Utility(_appDbContext);
            utility.UpdateMarketPrice();
            Console.WriteLine("Market Price Update Successfully!!!");
        }

    }
    public class Utility
    {
        private static ApplicationDbContext _appDbContext;
        private readonly Random _random = new Random();

        public Utility(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;
        }
        public bool UpdateMarketPrice()
        {
            bool s = false;
            try
            {
                List<Stocks> stocks = null;
                stocks = _appDbContext.Stocks.ToList();
                foreach (var item in stocks)
                {
                    int price = _random.Next(1, 100);
                    //item.pr = price;
                    _appDbContext.SaveChanges();
                }
                s = true;
            }
            catch (Exception ex)
            {

            }
            return s;
        }
    }

}
