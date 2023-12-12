using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class dtoBasketResult
    {
        public dtoBasketResult() 
        {
            Campaigns = new List<TblCampaign>();
        }

        public List<TblCampaign> Campaigns { get; set; }

        public TblUser User { get; set; }
        public IEnumerable<TblProduct> Products { get; set; }
        public decimal TotalStock { get { return Products == null ? 0 : Products.Sum(a => a.Stock); } }
         public decimal TotalAmount { get { return Products == null ? 0 : (Products.Sum(a => a.Price) * TotalStock); } }        
        public decimal Discount { get; set; } = 0;
        public decimal NetAmount { get { return TotalAmount - Discount; } }

    }
}
