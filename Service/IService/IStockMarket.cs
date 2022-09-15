using CompanyStockApi.Data;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IStockMarket
    {
        public IStocksModel Submit(IStocksModel stocksModel);
        public List<IStocksModel> GetStock();
        public Stocks GetStockSymbolbySymbol(string symbol);
        public string GetStockSymbolByID(int symID);
        public List<IStocksModel> GetStockSymbol(int dataLimit);
        public bool Delete(int symID);
        public List<PriceVariationResponseModel> FetchPriceVariation(PriceVariationModel priceVariation);
        public Task<bool> SubmitMarketPrice(MarketPriceUpdateModel marketPriceUpdate);
    }
}
