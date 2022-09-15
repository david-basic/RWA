using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [Serializable]
    public class AptOwnerMVC
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public IList<AptMVC> Apartments { get; set; }
    }
}
