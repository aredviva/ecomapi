using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace TheaTechEComAPI.Controllers
{
    public class ProductsController : _BaseAPI
    {
        private readonly ILogicServices svc;
        public ProductsController(ILogicServices svc)
        {
            this.svc = svc;
        }



        [HttpPost("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var _products = svc.SeedDataSvc.GetProducts();

            return Ok(_products);
        }

    }
}
