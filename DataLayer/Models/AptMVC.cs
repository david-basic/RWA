using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [Serializable]
    public class AptMVC
    {
        public static Comparison<AptMVC> PriceLowToHighComp = ((left, right) => left.Price.CompareTo(right.Price));
        public static Comparison<AptMVC> PriceHighToLowComp = ((left, right) => -left.Price.CompareTo(right.Price));
        public static Comparison<AptMVC> TotalRoomsLowToHighComp = ((left, right) => left.TotalRooms.CompareTo(right.TotalRooms));
        public static Comparison<AptMVC> TotalRoomsHighToLowComp = ((left, right) => -left.TotalRooms.CompareTo(right.TotalRooms));
        public static Comparison<AptMVC> TotalSpaceLowToHighComp = ((left, right) => (left.MaxAdults + left.MaxChildren).CompareTo(right.MaxAdults + right.MaxChildren));
        public static Comparison<AptMVC> TotalSpaceHighToLowComp= ((left, right) => -(left.MaxAdults + left.MaxChildren).CompareTo(right.MaxAdults + right.MaxChildren));
        public static IDictionary<string, Comparison<AptMVC>> ComparisonDictionary
        {
            get
            {
                return new Dictionary<string, Comparison<AptMVC>>()
                {
                    { "PLH", PriceLowToHighComp },
                    { "PHL", PriceHighToLowComp },
                    { "RLH", TotalRoomsLowToHighComp },
                    { "RHL", TotalRoomsHighToLowComp },
                    { "SLH", TotalSpaceLowToHighComp },
                    { "SHL", TotalSpaceHighToLowComp },
                };
            }
        }
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public City City { get; set; } = new City();
        public int CityId { get; set; }
        public string CityName { get => this.City.Name; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public decimal Price { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public int BeachDistance { get; set; }
        public IList<Tag> Tags { get; set; }
        public bool IsAvailable { get => StatusId == 3; }
        public ApartmentStatus Status { get; set; }
        public IList<AptPicMVC> AptPic { get; set; }
        public AptPicMVC MainPicture
        {
            get
            {
                var mainPic = AptPic.FirstOrDefault(picture => picture.IsRepresentative);

                if (mainPic is null)
                {
                    mainPic = AptPic[0];
                }

                return mainPic;
            }
        }
        public int PicturesCount { get => AptPic.Count; }
        public IList<ApartmentReview> Reviews { get; set; }
        public int TotalReviews
        {
            get
            {
                int totalReviews = 0;

                if (Reviews != null && Reviews.Count != 0)
                {
                    totalReviews = Reviews.Count;
                }

                return totalReviews;
            }
        }
        public int Stars
        {
            get
            {
                double average = 0.0;

                if (Reviews != null && Reviews.Count != 0)
                {
                    average = (Reviews.Select(r => r.Stars)).Average();

                    if ((double)(average - (int)average) >= 0.5)
                    {
                        average += 1.0;
                    }
                }
                return (int)average;
            }
        }
        public int TotalPeople
        {
            get
            {
                return MaxAdults + MaxChildren;
            }
        }
        public string PriceStringFormat { get => string.Format("{0:0.00}", Price) + "HRK"; }
    }
}
