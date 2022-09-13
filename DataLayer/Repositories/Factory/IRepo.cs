﻿using DataLayer.Models;
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
        IList<Apartment> LoadApartments();
        Apartment LoadApartmentById(int id);
        void InsertApartment(Apartment apartment);
        void DeleteApartment(Apartment apartment);
        void UpdateApartment(Apartment apartment, IList<ApartmentPicture> picturesToRemove);
        IList<Apartment> LoadApartments(params Predicate<Apartment>[] filters);
        IList<Apartment> LoadApartmentsByOwnerId(int id);
        IList<ApartmentOwner> LoadApartmentOwners();
        ApartmentOwner LoadApartmentOwnerById(int id);
        void InsertApartmentOwner(ApartmentOwner apartmentOwner);
        IList<string> LoadApartmentNames();
        IList<ApartmentPicture> LoadApartmentPictures();
        ApartmentPicture LoadApartmentPictureById(int id);
        void InsertApartmentPicture(ApartmentPicture apartmentPicture);
        void DeleteApartmentPicture(Guid guid);
        IList<ApartmentPicture> LoadApartmentPicturesByApartmentId(int id);
    }
}
