using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ISeedDataService _svc;
        public ProductServices(ISeedDataService svc)
        {
            this._svc = svc;
        }
        public async Task<IEnumerable<TblProduct>> GetProducts()
        {
            try
            {
                var _products = _svc.GetProducts();

                return _products;

            } catch(Exception ex)
            {
                return new List<TblProduct>();
            }
        }
    }
}
