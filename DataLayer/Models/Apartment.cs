using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? MaxAdults { get; set; }
        public int? MaxChildren { get; set; }
        public int? TotalRooms { get; set; }
        public int? BeachDistance { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ApartmentPicture> ApartmentPictures { get; set; }
    }
}
