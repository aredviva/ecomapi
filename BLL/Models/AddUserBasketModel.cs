using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AddUserBasketModel
    {
        public int? UserId { get; set; }
        public int ProductId { get; set; }

        public int Count { get; set; }

    }
}
