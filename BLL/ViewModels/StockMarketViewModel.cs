using CompanyStockApi.Data;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class StockMarketViewModel
    {
        private ApplicationDbContext applicationDbContext;
        private IStockMarket IstockMarket;
        public StockMarketViewModel(ApplicationDbContext _applicationDbContext, IStockMarket stockMarket)
        {
            applicationDbContext = _applicationDbContext;
            IstockMarket = stockMarket;
        }
        public bool Delete(int symID)
        {
            bool result = false;
            Stocks stocks = null;
            try
            {
                result = IstockMarket.Delete(symID);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public IStocksModel Submit(IStocksModel stocksModel)
        {
            try
            {
                var s = IstockMarket.Submit(stocksModel);
                stocksModel = s;
            }
            catch (Exception ex)
            {
            }
            return stocksModel;
        }

        public List<IStocksModel> GetStock()
        {
            List<IStocksModel> stocksModel = null;
            try
            {

                var s = IstockMarket.GetStock();
                stocksModel = s;
            }
            catch (Exception ex)
            {
            }
            return stocksModel;
        }
        public Stocks GetStockSymbolbySymbol(string symbol)
        {
            Stocks marketSymbol = null;
            try
            {
                var s = IstockMarket.GetStockSymbolbySymbol(symbol);
                marketSymbol = s;
            }
            catch (Exception ex)
            {
            }
            return marketSymbol;
        }

        public string GetStockSymbolByID(int symID)
        {
            string marketSymbol = null;
            try
            {
                var s = IstockMarket.GetStockSymbolByID(symID);
                marketSymbol = s;
            }
            catch (Exception ex)
            {


            }
            return marketSymbol;
        }

        public List<IStocksModel> GetStockSymbol(int dataLimit)
        {

            List<IStocksModel> marketSymbols = null;
            try
            {
                var s = IstockMarket.GetStockSymbol(dataLimit);
                marketSymbols = s;
            }
            catch (Exception ex)
            {
            }
            return marketSymbols;
        }
        public List<PriceVariationResponseModel> GetPriceVariation(PriceVariationModel priceVariation)
        {
            List<PriceVariationResponseModel> stocksModel = null;
            try
            {
                var s = IstockMarket.FetchPriceVariation(priceVariation);
                stocksModel = s;
            }
            catch (Exception ex)
            {
            }
            return stocksModel;
        }
        public async Task<bool> SubmitMaeketPrice(MarketPriceUpdateModel priceVariation)
        {
            bool st = false;
            try
            {
                await Task.Run(() =>
                {
                    var s = IstockMarket.SubmitMarketPrice(priceVariation).Result;
                    st = s;
                });
            }
            catch (Exception ex)
            {
            }
            return st;
        }
    }
}
