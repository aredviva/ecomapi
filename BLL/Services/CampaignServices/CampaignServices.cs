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
    public class CampaignServices : ICampaignServices
    {
   
        public CampaignServices()
        {
          
        }

        public async Task<ApiServiceResult<dtoBasketResult>> ApplyCampaigns(dtoBasketResult obj)
        {
            var _camps = await GetCampaigns();
                        
            foreach(var camp in _camps) 
            {
                CampaignsMethdos(camp, obj);
            }
            
          
            return new ApiServiceResult<dtoBasketResult>(obj);
        }

        public async Task<IEnumerable<TblCampaign>> GetCampaigns()
        {
            var _camps = new List<TblCampaign>()
            {
                new TblCampaign() {  CampaignName = "1000 TL > Total Amount =  %10 Discont", DiscontTriggerLimit = 1000.0m, DiscontValue = 10, IsStack = false, IsLowProductDiscount = true },
                new TblCampaign() {  CampaignName = "6x = 1x Free", DiscontTriggerLimit = 6.0m, DiscontValue = 1, IsStack = false, IsOneFreeDiscount = false  },
                new TblCampaign() {  CampaignName = "Product > 2 = (Low Price) %20 Discont", DiscontTriggerLimit = 2, DiscontValue = 20, IsPercent = true, IsStack = true, IsMultipliProducttDiscount  = true  },
            };

            return _camps;
        }

        private void CampaignsMethdos(TblCampaign camp, dtoBasketResult bask)
        {
            if((camp.IsLowProductDiscount && camp.IsStack == false && bask.Campaigns.Count == 0) || (camp.IsLowProductDiscount && camp.IsStack == true && bask.Campaigns.Count > 0))
            {
                if(bask.TotalAmount >= camp.DiscontTriggerLimit)
                {
                    var _dis_val = (bask.TotalAmount * (decimal.Parse("0." + camp.DiscontValue)));
                                        
                    bask.Discount = _dis_val;

                    bask.Campaigns.Add(camp);
                }

            } else if((camp.IsOneFreeDiscount  && camp.IsStack == false && bask.Campaigns.Count == 0) || (camp.IsOneFreeDiscount && camp.IsStack == true && bask.Campaigns.Count > 0)) {
              
                 foreach(var p in bask.Products.Where(a=>a.Stock >= camp.DiscontTriggerLimit))
                {
                    bask.Discount += p.Price;
                    camp.CampaignName = $"camp.CampaignName 1x {p.ProductName} Free";
                    bask.Campaigns.Add(camp);
                }              


            } else if((camp.IsMultipliProducttDiscount && camp.IsStack == false && bask.Campaigns.Count == 0) ||(camp.IsMultipliProducttDiscount && camp.IsStack == true && bask.Campaigns.Count > 0)) {
                
                if (bask.Products.Select(a=>a.Stock).Sum() >= camp.DiscontTriggerLimit)
                {
                    var _lowPriceProduct =  bask.Products.OrderBy(a=>a.Price).FirstOrDefault();

                    var _dis_val = (_lowPriceProduct.Price * (decimal.Parse("0." + camp.DiscontValue)));

                    bask.Discount = _dis_val;

                    bask.Campaigns.Add(camp);
                }

            }

        }

    }
}
