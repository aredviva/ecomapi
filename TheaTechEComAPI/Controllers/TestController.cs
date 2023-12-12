using APIServiceResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheaTechEComAPI.Controllers
{
    [Authorize]
    public class TestController : _BaseAPI
    {
        public TestController() 
        {
            
        }

  

        
        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {

            return Ok(new ApiServiceResult<string>("Login success"));
        }
    }
}
