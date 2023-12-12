using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SeedDataService : ISeedDataService
    {
        private static List<TblUser> _Users = new List<TblUser>()
        {
            new TblUser() {  Id = 1, Email = "a@a.com", Password = "1234" },
            new TblUser() {  Id = 2, Email = "b@b.com", Password = "1234" },
            new TblUser() {  Id = 3, Email = "c@c.com", Password = "1234" },
        };

        private static List<TblProduct> _Products = new List<TblProduct>()
        {
                new TblProduct() { Id = 1, ProductName = "PlayStation", Price = 175.0m, Stock = 20 },
                new TblProduct() { Id = 2, ProductName = "Nintendo", Price = 125.0m, Stock = 30 },
                new TblProduct() { Id = 3, ProductName = "Sega", Price = 90.0m, Stock = 40 },
        };

        private static List<TblBasket> _Baskets = new List<TblBasket>();

        public List<TblBasket> GetBasket()
        {
            return _Baskets;
        }

        public  IEnumerable<TblProduct> GetProducts()
        {
            return _Products;
        }

        public IEnumerable<TblUser> GetUsers()
        {
            return _Users;
        }

     
    }
}
