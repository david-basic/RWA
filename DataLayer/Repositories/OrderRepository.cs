using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class OrderRepository
    {
        public List<Models.Order> GetOrders()
        {
            return new List<Models.Order>
            {
                new Models.Order { Id = 0, Name = "(kriterij sortiranja)" },
                new Models.Order { Id = 1, Name = "Broj soba" },
                new Models.Order { Id = 2, Name = "Broj odraslih" },
                new Models.Order { Id = 3, Name = "Broj djece" },
                new Models.Order { Id = 4, Name = "Cijena" }
            };
        }
    }
}
