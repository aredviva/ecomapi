using Microsoft.AspNetCore.Mvc;
using ObjectFarm.BLL;

namespace TheaTechEComAPI.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class _BaseAPI : Controller
    {
        
        protected int UserID()
        {
            return HttpContext.GetUserID(); 
        }
    }
}
