using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TblCampaign
    {


        public string CampaignName { get; set; }

        


        // Discont Property
        public decimal DiscontTriggerLimit { get; set; } = 0;
        public decimal DiscontValue { get; set; }        
        public bool IsStack { get; set; } = false;
        public bool IsPercent { get; set; } = false;


        // Discount Setting
        public bool IsOneFreeDiscount { get; set; } = false;
        public bool IsLowProductDiscount { get; set; } = false;
        public bool IsMultipliProducttDiscount { get; set; } = false;
        

    }
}
