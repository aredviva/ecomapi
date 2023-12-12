using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ILogicServices
    {
        ISeedDataService SeedDataSvc { get; }
        IProductServices ProductServices { get; }
        IBasketService  BasketService { get; }

        ICampaignServices CampaignServices { get; }

    }
}
