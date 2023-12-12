using APIServiceResult;
using BLL.DTOs;
using BLL.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BasketService : IBasketService
    {

        private readonly ISeedDataService _svc;
        public BasketService(ISeedDataService seedSvc, IProductServices productSvc)
        {
            this._svc = seedSvc;
        }

        public async Task<ApiServiceResult<bool>> AddUserBasket(AddUserBasketModel obj)
        {
            if (_svc.GetProducts().Where(a => a.Id == obj.ProductId).Any() == false)
                return new ApiServiceResult<bool>(false, true, $"Product Not Found (Id:{obj.ProductId})");

            if (_svc.GetUsers().Where(a => a.Id == obj.UserId).Any() == false)
                return new ApiServiceResult< 
                    bool>(false, true, $"User Not Found (Id: {obj.UserId})");

            
            var _currentBasket = GetCurrentUserBasket(obj.UserId.Value);
            var _count = GetUserBasketProductCount(obj.UserId.Value, obj.ProductId);
            var _product = _svc.GetProducts().Where(a => a.Id == obj.ProductId).SingleOrDefault();

            if (_product == null) return new ApiServiceResult<bool>(false, true, $"Product Not Found (Id:{obj.ProductId})");
            
            if (obj.Count > _product.Stock) 
                return new ApiServiceResult<bool>(false, true, $"There is not enough stock. You can buy a maximum of {_product.Stock}.");

             var _currBasket = _svc.GetBasket().Where(a=>a.UserId == obj.UserId && a.ProductId == obj.ProductId).SingleOrDefault();

            if(_currBasket == null) { 

            _product.Stock -= obj.Count;            
            

            var _basket = new TblBasket()
            {
                 ProductId = obj.ProductId,
                 Count = obj.Count,
                  UserId = obj.UserId.Value
            };

            _svc.GetBasket().Add(_basket);

            } else  {

                _currBasket.Count += obj.Count;

            }


            return new ApiServiceResult<bool>(true);
        }

        public async Task<ApiServiceResult<bool>> RemoveUserBasket(AddUserBasketModel obj)
        {
            if (_svc.GetProducts().Where(a => a.Id == obj.ProductId).Any() == false)
                return new ApiServiceResult<bool>(false, true, $"Product Not Found (Id:{obj.ProductId})");

            if (_svc.GetUsers().Where(a => a.Id == obj.UserId).Any() == false)
                return new ApiServiceResult<bool>(false, true, $"User Not Found (Id: {obj.UserId})");

            var _product = _svc.GetProducts().Where(a => a.Id == obj.ProductId).SingleOrDefault();
            if (_product == null) return new ApiServiceResult<bool>(false, true, $"Product Not Found (Id:{obj.ProductId})");

            var _basketProducts = _svc.GetBasket().Where(a => a.UserId == obj.UserId && obj.ProductId == obj.ProductId)
                .SingleOrDefault();

            if (_basketProducts == null) return new ApiServiceResult<bool>(false, true, "User Basket Empty");

            if (_basketProducts.Count == 0) return new ApiServiceResult<bool>(true);

                    
            
            if(obj.Count == 0)
            {
                _svc.GetBasket().RemoveAll(a=>a.UserId == obj.UserId && obj.ProductId == obj.ProductId);

                _product.Stock += _basketProducts.Count;

            } else  
            {
                var _subCount = _basketProducts.Count;


                _basketProducts.Count -=  obj.Count;
                
                _product.Stock += obj.Count;

                if (_basketProducts.Count == 0) _svc.GetBasket().Remove(_basketProducts);
            }


            return new ApiServiceResult<bool>(true);
        }

        public async Task<ApiServiceResult<dtoBasketResult>> GetUserBasket(int UserId)
        {
            if (_svc.GetUsers().Where(a => a.Id == UserId).Any() == false)
                return new ApiServiceResult<dtoBasketResult>(null, true, $"User Not Found (Id: {UserId})");

            var _user = _svc.GetUsers().Where(a => a.Id == UserId).SingleOrDefault();
            if (_user == null) return new ApiServiceResult<dtoBasketResult>(null, true, $"User Not Found (Id:{UserId})");

            var _basket = GetCurrentUserBasket(UserId);
        
            var res = new dtoBasketResult();

            List<TblProduct> _currentProduct = new List<TblProduct>();
            foreach(var _b in _basket)
            {
                var _product = _svc.GetProducts().Where(a => a.Id == _b.ProductId).SingleOrDefault();

                _currentProduct.Add(new TblProduct()
                {
                     ProductName = _product.ProductName,
                     Stock = _b.Count,
                     Price = _product.Price,    
                     Id = _b.ProductId
                });


            }



            res.Products = _currentProduct;
            res.User = _user;
       

            return new ApiServiceResult<dtoBasketResult>(res);
        }

        private IEnumerable<TblBasket> GetCurrentUserBasket(int UserId)
        {
            return _svc.GetBasket().Where(a => a.UserId == UserId);
        }

        private int GetUserBasketProductCount(int UserId, int ProductId)
        {
            return _svc.GetBasket().Where(a=>a.UserId == UserId && a.ProductId == ProductId).Count();
            
        }
    }
}
