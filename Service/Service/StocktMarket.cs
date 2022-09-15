using CompanyStockApi.Data;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Service.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Service
{
    public class StocktMarket : IStockMarket
    {
        private ApplicationDbContext applicationDbContext;
        public StocktMarket(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public bool Delete(int symID)
        {
            bool result = false;
            Stocks stocks = null;
            try
            {
                if (symID != 0)
                {
                    stocks = applicationDbContext.Stocks.Where(a => a.Id == symID).FirstOrDefault();
                    if (stocks != null)
                    {
                        stocks.Active = false;
                        stocks.UpdateDate = DateTime.Now;
                    }
                    var response = applicationDbContext.SaveChanges();
                    result = true;
                }
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
                Stocks stocks = null;
                MarketPriceDetails market = null;
                if (stocksModel != null)
                {
                    stocks = applicationDbContext.Stocks.Where(a => a.Id == stocksModel.Id).FirstOrDefault();
                    if (stocks != null)
                    {
                        market = applicationDbContext.MarketPriceDetails.Where(a => a.StocksMarketID == stocks.Id).FirstOrDefault();
                    }
                    if (stocks != null)
                    {
                        stocks.CompanyName = stocksModel.CompanyName;
                        stocks.SymbolName = stocksModel.SymbolName;
                        stocks.Active = stocksModel.Active;
                        stocks.UpdateDate = DateTime.Now;
                        if (market != null)
                        {
                            market.Price = stocksModel.CurrentPrice;

                        }
                        else
                        {
                            market = new MarketPriceDetails()
                            {
                                Price = stocksModel.CurrentPrice,
                                CreatedDate = DateTime.Now,
                                StocksMarketID = stocks.Id,
                            };
                            applicationDbContext.MarketPriceDetails.Add(market);
                            var marketResponse = applicationDbContext.SaveChanges();
                            int marketid = market.ID;
                        }
                    }
                    else
                    {

                        stocks = new Stocks()
                        {
                            CompanyName = stocksModel.CompanyName,
                            SymbolName = stocksModel.SymbolName,
                            Active = stocksModel.Active,
                            CreatedDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                        };
                        applicationDbContext.Stocks.Add(stocks);

                    }
                }
                var response = applicationDbContext.SaveChanges();
                stocksModel.Id = stocks.Id;
                if (stocks.Id > 0)
                {
                    if (market == null)
                    {
                        market = new MarketPriceDetails()
                        {
                            Price = stocksModel.CurrentPrice,
                            CreatedDate = DateTime.Now,
                            StocksMarketID = stocks.Id,
                        };
                        applicationDbContext.MarketPriceDetails.Add(market);
                        var marketResponse = applicationDbContext.SaveChanges();
                        int marketid = market.ID;

                    }

                }
            }
            catch (Exception ex)
            {
            }
            return stocksModel;
        }

        List<IStocksModel> IStockMarket.GetStock()
        {
            List<IStocksModel> stocksModel = null;
            try
            {

                var stocks = applicationDbContext.Stocks.Where(s => s.Active == true).ToList();
                if (stocks != null)
                {
                    stocksModel = new List<IStocksModel>();

                    foreach (var item in stocks)
                    {
                        var marketSymbol = applicationDbContext.MarketPriceDetails.Where(s => s.StocksMarketID == item.Id).OrderByDescending(a => a.ID).FirstOrDefault();
                        stocksModel.Add(new IStocksModel()
                        {
                            Active = item.Active,
                            CompanyName = item.CompanyName,
                            CurrentPrice = marketSymbol.Price,
                            Symbol = item != null ? item.SymbolName : String.Empty,
                            SymbolName = item != null ? item.SymbolName : String.Empty,
                            Id = item.Id,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return stocksModel;
        }
        Stocks IStockMarket.GetStockSymbolbySymbol(string symbol)
        {
            Stocks marketSymbol = null;
            try
            {
                var stocks = applicationDbContext.Stocks.Where(s => s.SymbolName == symbol).FirstOrDefault();
                if (stocks != null)
                {
                    marketSymbol = new Stocks();
                    marketSymbol.SymbolName = stocks.SymbolName;
                    marketSymbol.Active = stocks.Active;
                    marketSymbol.CreatedDate = stocks.CreatedDate;
                    marketSymbol.UpdateDate = stocks.UpdateDate;
                    marketSymbol.Id = stocks.Id;
                }
            }
            catch (Exception ex)
            {


            }
            return marketSymbol;
        }
        string IStockMarket.GetStockSymbolByID(int symID)
        {
            string marketSymbol = string.Empty;
            try
            {
                var stocks = applicationDbContext.Stocks.Where(s => s.Id == symID).Select(s => s.SymbolName).FirstOrDefault();
                if (stocks != null)
                {
                    marketSymbol = stocks;
                }
            }
            catch (Exception ex)
            {


            }
            return marketSymbol;
        }

        public List<IStocksModel> GetStockSymbol(int dataLimit)
        {

            //List<string> marketSymbols = null;
            //try
            //{
            //    if (dataLimit == 0)
            //    {
            //        marketSymbols = applicationDbContext.Stocks.Select(l => l.SymbolName).ToList();
            //    }
            //    else
            //    {
            //        marketSymbols = applicationDbContext.Stocks.Select(l => l.SymbolName).ToList();
            //        marketSymbols = marketSymbols.Take(dataLimit).ToList();
            //    }

            //}
            //catch (Exception ex)
            //{
            //}
            //return marketSymbols;

            List<IStocksModel> stocksModel = null;
            try
            {
                List<Stocks> stocks = null;


                if (dataLimit == 0)
                {
                    stocks = applicationDbContext.Stocks.Where(s => s.Active == true).ToList();
                }
                else
                {
                    stocks = applicationDbContext.Stocks.Where(s => s.Active == true).ToList();
                    stocks = stocks.Take(dataLimit).ToList();
                }
                if (stocks != null)
                {
                    stocksModel = new List<IStocksModel>();

                    foreach (var item in stocks)
                    {
                        var marketSymbol = applicationDbContext.MarketPriceDetails.Where(s => s.StocksMarketID == item.Id).OrderByDescending(a => a.ID).FirstOrDefault();


                        stocksModel.Add(new IStocksModel()
                        {
                            Active = item.Active,
                            CompanyName = item.CompanyName,
                            CurrentPrice = marketSymbol != null ? marketSymbol.Price : 0,
                            Symbol = item != null ? item.SymbolName : String.Empty,
                            SymbolName = item != null ? item.SymbolName : String.Empty,
                            Id = item.Id,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return stocksModel;
        }

        public List<PriceVariationResponseModel> FetchPriceVariation(PriceVariationModel priceVariation)
        {
            List<PriceVariationResponseModel> priceVariationList = new List<PriceVariationResponseModel>();
            try
            {
                if (priceVariation != null)
                {

                    var getPriceList = applicationDbContext.MarketPriceDetails.ToList();
                    if (getPriceList != null)
                    {
                        if (priceVariation.Hour != 0)
                        {
                            getPriceList = getPriceList.Where(a => a.CreatedDate.Hour == priceVariation.Hour).ToList();
                        }
                        if (priceVariation.Day != 0)
                        {
                            getPriceList = getPriceList.Where(a => a.CreatedDate.Day == priceVariation.Day).ToList();
                        }
                        if (priceVariation.Week != 0)
                        {
                            getPriceList = getPriceList.Where(a => a.CreatedDate.DayOfWeek == priceVariation.Week).ToList();
                        }
                        if (priceVariation.Month != 0)
                        {
                            getPriceList = getPriceList.Where(a => a.CreatedDate.Month == priceVariation.Month).ToList();
                        }
                        foreach (var item in getPriceList)
                        {
                            var stock = applicationDbContext.Stocks.Where(a => a.Id == item.StocksMarketID).FirstOrDefault();
                            priceVariationList.Add(new PriceVariationResponseModel()
                            {
                                Price = item.Price,
                                PriceUpdateDate = item.CreatedDate,
                                SymbolName = stock != null ? stock.SymbolName : null,
                            });
                        }
                    }
                }


            }
            catch (Exception ex)
            {


            }
            return priceVariationList;
        }

        public async Task<bool> SubmitMarketPrice(MarketPriceUpdateModel marketPriceUpdate)
        {
            bool s = false;
            await Task.Run(() =>
            {
                MarketPriceDetails marketPriceDTo = null;
                try
                {
                    if (marketPriceUpdate != null)
                    {
                        double price = 0;
                        Double d = 10;
                        marketPriceDTo = applicationDbContext.MarketPriceDetails.Where(f => f.StocksMarketID == marketPriceUpdate.SymbolId).OrderByDescending(a => a.Price).FirstOrDefault();
                        if (marketPriceDTo != null)
                        {
                            price = marketPriceDTo.Price / d;
                            if (marketPriceUpdate.OpertaionforPrice == '+')
                            {
                                price = price + marketPriceDTo.Price;
                            }
                            if (marketPriceUpdate.OpertaionforPrice == '-')
                            {
                                price = marketPriceDTo.Price - price;
                            }

                        }
                        marketPriceDTo = new MarketPriceDetails()
                        {
                            CreatedDate = DateTime.Now,
                            Price = price,
                            StocksMarketID = marketPriceUpdate.SymbolId
                        };
                        applicationDbContext.MarketPriceDetails.AddAsync(marketPriceDTo);
                        int marketResponse = applicationDbContext.SaveChangesAsync().Result;
                        if (marketResponse != 0)
                        {
                            s = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            });
            return s;
        }

        public async Task<bool> UpdateAllMarketPrice(char Operation)
        {
            bool s = false;
            await Task.Run(() =>
            {
                MarketPriceDetails marketPriceDTo = null;
                try
                {

                    double price = 0;
                    Double d = 10;
                    List<Stocks> stocks = null;
                    stocks = applicationDbContext.Stocks.Where(s => s.Active == true).ToList();
                    foreach (Stocks stock in stocks)
                    {
                        marketPriceDTo = applicationDbContext.MarketPriceDetails.Where(f => f.StocksMarketID == stock.Id).OrderByDescending(a => a.Price).FirstOrDefault();
                        if (marketPriceDTo != null)
                        {
                            if (marketPriceDTo.Price==0)
                            {
                                marketPriceDTo.Price = 10;
                            }
                            price =  marketPriceDTo.Price / d;
                            if (Operation == '+')
                            {
                                price = price + marketPriceDTo.Price;
                            }
                            if (Operation == '-')
                            {
                                price = marketPriceDTo.Price - price;
                            }

                        }
                        marketPriceDTo = new MarketPriceDetails()
                        {
                            CreatedDate = DateTime.Now,
                            Price = price,
                            StocksMarketID = stock.Id
                        };
                        applicationDbContext.MarketPriceDetails.AddAsync(marketPriceDTo);
                        int marketResponse = applicationDbContext.SaveChangesAsync().Result;
                        if (marketResponse != 0)
                        {
                            s = true;
                        }
                    }
                    

                }
                catch (Exception ex)
                {

                }
            });
            return s;
        }
    }
}
