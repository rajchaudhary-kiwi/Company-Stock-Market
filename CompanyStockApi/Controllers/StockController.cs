using BLL.ViewModels;
using CompanyStockApi.Data;
using CompanyStockApi.Models;
using DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using controllerModel = CompanyStockApi.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private StockMarketViewModel stockMarketViewModel;
        public StockController(StockMarketViewModel stockMarketView)
        {
            stockMarketViewModel = stockMarketView;
        }

        // GET: api/Stock
        [HttpGet]
        public List<StockModelUpdate> Get()
        {
            List<StockModelUpdate> stocks = null;
            var response = stockMarketViewModel.GetStock();
            if (response != null)
            {
                stocks = new List<StockModelUpdate>();
                foreach (var item in response)
                {
                    stocks.Add(new StockModelUpdate()
                    {
                        CompanyName = item.CompanyName,
                        Symbol = item.Symbol,
                        Active = item.Active,
                        CurrentPrice = item.CurrentPrice,
                        Id = item.Id,

                    });
                }
            }

            return stocks;
        }

        [HttpGet("GetStockSymbolUsingPagination/{length}")]
        public List<IStocksModel> GetStockSymbolUsingPagination(int length)
        {
            List<IStocksModel> stocks = null;
            var response = stockMarketViewModel.GetStockSymbol(length);
            if (response != null)
            {
                stocks = new List<IStocksModel>();
                foreach (var item in response)
                {
                    stocks.Add(new IStocksModel()
                    {
                        Active = item.Active,
                        CompanyName = item.CompanyName,
                        CurrentPrice = item.CurrentPrice,
                        Id = item.Id,
                        SymbolName = item.SymbolName,

                    });
                }
            }

            return response;
        }
        [HttpPost("PriceVariation")]
        public List<PriceVariationResponseModel> PriceVariation([FromBody] PriceVariationModel priceVariation)
        {
            var ss = DateTime.Now;
            List<PriceVariationResponseModel> priceVariationResponseModels = null;
            priceVariationResponseModels = stockMarketViewModel.GetPriceVariation(priceVariation);
            return priceVariationResponseModels;
        }

        static MarketPriceRequeestModel marketPriceRequest;
        static int count = 0;

        [HttpPost("PriceRandomChange")]
        public async Task<bool> PriceRandomChange([FromBody] MarketPriceRequeestModel marketPriceRequeestModel)
        {
            bool s = false;
            int interval = 1000;
            while (true)
            {
                if (marketPriceRequeestModel != null)
                {
                    marketPriceRequest = marketPriceRequeestModel;
                    if (marketPriceRequest.StartTime < DateTime.Now)
                    {

                        await Task.Run(() =>
                        {
                            MarketPriceUpdateModel marketPriceUpdateModel = new MarketPriceUpdateModel()
                            {
                               // SymbolId = marketPriceRequest.SymbolId,
                            };

                            if (count == 3 || count == 7 || count == 11)
                            {
                                marketPriceUpdateModel.OpertaionforPrice = '-';
                            }
                            else
                            {
                                marketPriceUpdateModel.OpertaionforPrice = '+';
                            }

                            var re = stockMarketViewModel.SubmitMaeketPrice(marketPriceUpdateModel).Result;
                            s = re;
                        });
                        count++;
                        int r = interval * marketPriceRequeestModel.interevaltime;
                        Thread.Sleep(r);
                        if (marketPriceRequest.EndTime < DateTime.Now)
                        {
                            break;
                        }
                    }
                }
            }
            return s;
        }


        // GET: api/Stock/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            StockModelUpdate stock = null;
            var res = stockMarketViewModel.GetStockSymbolByID(id);

            return res;
        }

        // POST api/<StockController>
        [HttpPost]
        public int Post([FromBody] StockModelAdd stock)
        {

            IStocksModel Istocks = new IStocksModel()
            {
                CompanyName = stock.CompanyName,
                Active = stock.Active,
                CurrentPrice = stock.CurrentPrice,
                SymbolName = stock.Symbol,
                Symbol = stock.Symbol,
            };

            var s = stockMarketViewModel.Submit(Istocks);
            if (s != null && s.Id != 0)
            {
                return StatusCodes.Status201Created;
            }
            return StatusCodes.Status400BadRequest;
        }

        // PUT api/<StockController>
        [HttpPut]
        public StockModelUpdate Put([FromBody] StockModelUpdate stock)
        {
            IStocksModel Istocks = new IStocksModel()
            {
                CompanyName = stock.CompanyName,
                Active = stock.Active,
                CurrentPrice = stock.CurrentPrice,
                SymbolName = stock.Symbol,
                Symbol = stock.Symbol,
                Id = stock.Id,
            };
            var s = stockMarketViewModel.Submit(Istocks);
            return stock;

        }

        // DELETE api/<StockController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var s = stockMarketViewModel.Delete(id);
            return s;
        }
    }
}
