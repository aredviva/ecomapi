using APIServiceResult;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFarm.BLL
{
    
    public static class HttpContextExtensions
    {

        public static int GetUserID(this HttpContext ctx)
        {
            try
            {
                var _clams = ctx.User.Claims.Where(a => a.Type == "userId").SingleOrDefault();
                if (_clams == null) ctx.Response.Redirect("/unauthorized");
             
                return int.Parse(_clams.Value);
               
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
