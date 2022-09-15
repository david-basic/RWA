using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [Serializable]
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
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public decimal Price { get; set; }
        public string PriceStringFormat { get => string.Format("{0:0.00}", Price) + "HRK"; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public int TotalPeople
        {
            get
            {
                return MaxAdults + MaxChildren;
            }
        }
        public int? BeachDistance { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ApartmentPicture> ApartmentPictures { get; set; }
        public int PicturesCount { get => ApartmentPictures.Count; }
        public ApartmentPicture MainPicture
        {
            get
            {
                var representativePicture = ApartmentPictures.FirstOrDefault(picture => picture.IsRepresentative);

                if (representativePicture is null)
                {
                    representativePicture = ApartmentPictures[0];
                }

                return representativePicture;
            }
        }
        public ApartmentStatus Status { get; set; }
        public City City { get; set; } = new City();
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
        public bool IsAvailable { get => StatusId == 3; }

        public static Comparison<Apartment> PriceLowToHighComp = (left, right) => left.Price.CompareTo(right.Price);
        public static Comparison<Apartment> PriceHighToLowComp = (left, right) => -left.Price.CompareTo(right.Price);
        public static Comparison<Apartment> TotalRoomsLowToHighComp = (left, right) => left.TotalRooms.CompareTo(right.TotalRooms);
        public static Comparison<Apartment> TotalRoomsHighToLowComp = (left, right) => -left.TotalRooms.CompareTo(right.TotalRooms);
        public static Comparison<Apartment> TotalSpaceLowToHighComp = (left, right) => (left.MaxAdults + left.MaxChildren).CompareTo(right.MaxAdults + right.MaxChildren);
        public static Comparison<Apartment> TotalSpaceHighToLowComp = (left, right) => -(left.MaxAdults + left.MaxChildren).CompareTo(right.MaxAdults + right.MaxChildren);
        public static IDictionary<string, Comparison<Apartment>> ComparisonDictionary
        {
            get
            {
                return new Dictionary<string, Comparison<Apartment>>()
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
    }
}
