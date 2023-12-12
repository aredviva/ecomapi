using BLL.DTOs;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace TheaTechEComAPI.Controllers
{
    public class LoginController : _BaseAPI
    {
        readonly ILoginServices loginSvc;
        public LoginController(ILoginServices loginSvc) 
        {
            this.loginSvc = loginSvc;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel loginObj)
        {


            var res = await loginSvc.UserLogin(loginObj);
            
            return Ok(res);
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(UserLoginModel loginObj)
        {


            var res = await loginSvc.UserLogin(loginObj, true);

            return Ok(res);
        }


        [HttpPost("/unauthorized")]
        public async Task<IActionResult> Unauthorized()
        {

            return Unauthorized().Result;
        }

    }
}
