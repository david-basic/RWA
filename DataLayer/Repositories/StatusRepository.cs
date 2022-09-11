using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class StatusRepository
    {
        public List<Models.Status> GetStatuses()
        {
            return new List<Models.Status>
            {
                new Models.Status { Id = 0, Name = "(odabir statusa)" },
                new Models.Status { Id = 1, Name = "Zauzeto" },
                new Models.Status { Id = 2, Name = "Rezervirano" },
                new Models.Status { Id = 3, Name = "Slobodno" }
            };
        }
    }
}
