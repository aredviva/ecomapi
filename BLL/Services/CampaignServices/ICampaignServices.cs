using APIServiceResult;
using BLL.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICampaignServices
    {

        Task<IEnumerable<TblCampaign>> GetCampaigns();
        Task<ApiServiceResult<dtoBasketResult>> ApplyCampaigns(dtoBasketResult obj);
    }
}
