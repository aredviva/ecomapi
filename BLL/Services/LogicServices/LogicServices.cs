using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LogicServices : ILogicServices
    {
        public ISeedDataService SeedDataSvc { get; }
        public IProductServices ProductServices { get; }
        public IBasketService BasketService { get; }
        public ICampaignServices CampaignServices { get; }

        public LogicServices(ISeedDataService seedDataSvc) 
        { 
            this.SeedDataSvc = seedDataSvc;

            this.ProductServices    = new ProductServices(seedDataSvc);
            this.BasketService = new BasketService(seedDataSvc, ProductServices);            
            this.CampaignServices = new CampaignServices();

        }

        
    }
}
