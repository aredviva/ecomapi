using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectFarm.BLL;

namespace TheaTechEComAPI.Controllers
{
    [Authorize]
    public class BasketController : _BaseAPI
    {
        private readonly ILogicServices svc;
        
        public BasketController(ILogicServices svc)
        {
            this.svc = svc;
           
        }

        


        [HttpPost("getuserbasket")]
        public async Task<IActionResult> GetUserBasket()
        {
 

            var res =await svc.BasketService.GetUserBasket(UserID());

            res = await svc.CampaignServices.ApplyCampaigns(res.Data);


            return Ok(res);
        }


        [HttpPost("adduserbasket/{productId}/{count}")]
        public async Task<IActionResult> AddUserBasket(int productId, int count)
        {
       

            var res = await svc.BasketService.AddUserBasket(new AddUserBasketModel() { UserId = UserID(), ProductId = productId, Count = count });

            return Ok(res);
        }

        [HttpPost("removeuserbasket/{productId}/{count}")]
        public async Task<IActionResult> RemoveUserBasket(int productId, int count)
        {


            var res = await svc.BasketService.RemoveUserBasket(new AddUserBasketModel() { UserId = UserID(), ProductId = productId, Count = count });

            return Ok(res);
        }

    }
}
