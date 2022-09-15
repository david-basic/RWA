using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Factory
{
    public interface IRepo
    {
        IList<ApartmentStatus> LoadApartmentStatus();
        ApartmentStatus LoadApartmentStatusById(int id);
        void InsertApartmentStatus(ApartmentStatus apartmentStatus);
        IList<User> LoadUsers();
        User LoadUserById(int id);
        void InsertUser(User user);
        void DeleteUser(int id);
        User AuthUser(string username, string password);
        IList<City> LoadCities();
        City LoadCitiyById(int id);
        void InsertCity(City city);
        IList<TaggedApartment> LoadTaggedApartments();
        TaggedApartment LoadTaggedApartmentById(int id);
        void InsertTaggedApartment(TaggedApartment taggedApartment);
        void DeleteTaggedApartment(int id);
        IList<TagType> LoadTagTypes();
        TagType LoadTagTypeById(int id);
        IList<Tag> LoadTags();
        Tag LoadTagById(int id);
        void InsertTag(Tag tag);
        void DeleteTag(int id);
        IList<Tag> LoadTagsByApartmentId(int id);
        IList<Tuple<Tag, int>> LoadTagsCounted();
        void InsertTagType(TagType tagType);
        IList<ApartmentReservation> LoadApartmentReservations();
        ApartmentReservation LoadApartmentReservationById(int id);
        void InsertApartmentReservation(ApartmentReservation apartmentReservation);
        void GenerateApartmentReservation(ApartmentReservation apartmentReservation);
        IList<ApartmentReview> LoadApartmentReviews();
        IList<ApartmentReview> LoadApartmentReviewsByApartmentId(int id);
        ApartmentReview LoadApartmentReviewById(int id);
        void InsertApartmentReview(ApartmentReview apartmentReivew);
        IList<AptMVC> LoadApartments();
        AptMVC LoadApartmentById(int id);
        void InsertApartment(AptMVC apartment);
        void DeleteApartment(AptMVC apartment);
        void UpdateApartment(AptMVC apartment, IList<AptPicMVC> picturesToRemove);
        IList<AptMVC> LoadApartments(params Predicate<AptMVC>[] filters);
        IList<AptMVC> LoadApartmentsByOwnerId(int id);
        IList<ApartmentOwner> LoadApartmentOwners();
        ApartmentOwner LoadApartmentOwnerById(int id);
        void InsertApartmentOwner(ApartmentOwner apartmentOwner);
        IList<string> LoadApartmentNames();
        IList<AptPicMVC> LoadApartmentPictures();
        AptPicMVC LoadApartmentPictureById(int id);
        void InsertApartmentPicture(AptPicMVC apartmentPicture);
        void DeleteApartmentPicture(Guid guid);
        IList<AptPicMVC> LoadApartmentPicturesByApartmentId(int id);
    }
}
