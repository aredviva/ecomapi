using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<TblProduct>> GetProducts();
    }
}
