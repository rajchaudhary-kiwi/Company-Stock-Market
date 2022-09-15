using BLL.ViewModels;
using CompanyStockApi.Data;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceApptoUpdateMarketPrice
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static ApplicationDbContext _appDbContext;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var services = new ServiceCollection();
            string strCon = "Server=LAPTOP-FC8PBLOD\\SQLEXPRESS;Database=CompanyStockApi;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(strCon));

            var serviceProvider = services.BuildServiceProvider();
            _appDbContext = serviceProvider.GetService<ApplicationDbContext>();
            StocktMarket stocktMarketService = new StocktMarket(_appDbContext);

            int count = 0;
            DateTime StartTime =DateTime.Now.AddSeconds(5); // this is a constant value we can get this value from DB.
           
            while (!stoppingToken.IsCancellationRequested)
            {
                if (StartTime < DateTime.Now)
                {
                    char c = '+';
                    if (count == 3 || count == 7 || count == 11)
                    {
                        c = '-';
                    }
                    else
                    {
                        c = '+';
                    }
                    bool s = await stocktMarketService.UpdateAllMarketPrice(c);
                    _logger.LogInformation("Market Price Last Update at: {time}", DateTimeOffset.Now);
                    await Task.Delay(5000, stoppingToken);
                    
                }
                else
                {
                    _logger.LogInformation("Market Yet Not Start Please Wait Some Time to Market Start: {time}", StartTime);
                }
            }
        }
    }
}
