using APIServiceResult;
using BLL.DTOs;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ILoginServices
    {
        Task<ApiServiceResult<dtoUserLogin>> UserLogin(UserLoginModel model, bool IsRefreshToken = false);
    }
}
