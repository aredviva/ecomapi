using APIServiceResult;
using BLL.DTOs;
using BLL.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBasketService
    {
        Task<ApiServiceResult<dtoBasketResult>> GetUserBasket(int UserId);

        Task<ApiServiceResult<bool>> AddUserBasket(AddUserBasketModel obj);

        Task<ApiServiceResult<bool>> RemoveUserBasket(AddUserBasketModel obj);
    }
}
