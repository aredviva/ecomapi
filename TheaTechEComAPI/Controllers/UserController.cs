using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheaTechEComAPI.Controllers
{
    [Authorize]
    public class UserController : _BaseAPI
    {
        private readonly ILogicServices svc;
        public UserController(ILogicServices svc) 
        {
            this.svc = svc;
        }


        
        [HttpPost("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            var _usr = svc.SeedDataSvc.GetUsers();

            return Ok(_usr);
        }
    }
}
