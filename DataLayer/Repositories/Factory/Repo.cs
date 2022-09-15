using DataLayer.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Factory
{
    public partial class Repo : DataBaseRepoAbs
    {
        private int LoadApartmentIdByGuid(Guid guid)
        {
            int apartmentId = 1;
            var tblApartments = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentIdByGuid), guid).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartmentId = (int)(row[nameof(AptMVC.Id)]);
            }

            return apartmentId;
        }

        private void DeleteTaggedApartmentByApartmentId(int apartmentId)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteTaggedApartmentByApartmentId), apartmentId);
        }

        private void DeleteTaggedApartment(TaggedApartment taggedApartment)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteTaggedApartment), taggedApartment.ApartmentId, taggedApartment.TagId);
        }

        public override User AuthUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override void DeleteApartment(AptMVC apartment)
        {
            foreach (var tag in apartment.Tags)
            {
                this.DeleteTaggedApartment(new TaggedApartment { TagId = tag.Id, ApartmentId = apartment.Id });
            }

            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteApartment), apartment.Id);
        }

        public override void DeleteApartmentPicture(Guid guid)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteApartmentPicture), guid);
        }

        public override void DeleteTag(int id)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteTag), id);
        }

        // bad idea to delete tagged apartments
        public override void DeleteTaggedApartment(int id)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteTaggedApartment), id);
        }

        public override void DeleteUser(int id)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(DeleteUser), id);
        }

        public override void InsertApartment(AptMVC apartment)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertApartment),
                    apartment.Guid,
                    apartment.CreatedAt,
                    apartment.OwnerId,
                    apartment.StatusId,
                    apartment.CityId,
                    apartment.Address,
                    apartment.Name,
                    apartment.NameEng,
                    apartment.Price,
                    apartment.MaxAdults,
                    apartment.MaxChildren,
                    apartment.TotalRooms,
                    apartment.BeachDistance
                );

            int apartmentId = LoadApartmentIdByGuid(apartment.Guid);
            foreach (Tag tag in apartment.Tags)
                InsertTaggedApartment(new TaggedApartment { Guid = Guid.NewGuid(), ApartmentId = apartmentId, TagId = tag.Id });

            foreach (AptPicMVC picture in apartment.Pictures)
            {
                picture.ApartmentId = apartmentId;
                InsertApartmentPicture(picture);
            }
        }

        public override void InsertApartmentOwner(AptOwnerMVC apartmentOwner)
        {
            throw new NotImplementedException();
        }

        public override void InsertApartmentPicture(AptPicMVC apartmentPicture)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertApartmentPicture),
                    apartmentPicture.Guid,
                    apartmentPicture.CreatedAt,
                    apartmentPicture.ApartmentId,
                    apartmentPicture.Path,
                    apartmentPicture.Base64Content,
                    apartmentPicture.Name,
                    apartmentPicture.IsRepresentative
                );
        }

        public override void InsertApartmentReservation(ApartmentReservation apartmentReservation)
        {
            string details = apartmentReservation.Details;
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertApartmentReservation),
                    apartmentReservation.Guid,
                    apartmentReservation.CreatedAt,
                    apartmentReservation.ApartmentId,
                    apartmentReservation.Details,
                    apartmentReservation.UserId,
                    apartmentReservation.UserName,
                    apartmentReservation.UserEmail,
                    apartmentReservation.UserPhone,
                    apartmentReservation.UserAddress
                );
        }

        public override void InsertApartmentReview(ApartmentReview apartmentReivew)
        {
            apartmentReivew.Guid = Guid.NewGuid();
            apartmentReivew.CreatedAt = DateTime.Now;
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertApartmentReview),
                    apartmentReivew.Guid,
                    apartmentReivew.CreatedAt,
                    apartmentReivew.ApartmentId,
                    apartmentReivew.UserId,
                    apartmentReivew.Details,
                    apartmentReivew.Stars
                );
        }

        public override void InsertApartmentStatus(ApartmentStatus apartmentStatus)
        {
            throw new NotImplementedException();
        }

        public override void InsertCity(City city)
        {
            throw new NotImplementedException();
        }

        public override void InsertTag(Tag tag)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertTag), tag.Guid, tag.CreatedAt, tag.TypeId, tag.Name, tag.NameEng);
        }

        public override void InsertTaggedApartment(TaggedApartment taggedApartment)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertTaggedApartment), taggedApartment.Guid, taggedApartment.ApartmentId, taggedApartment.TagId);
        }

        public override void InsertTagType(TagType tagType)
        {
            throw new NotImplementedException();
        }
        public override void InsertUser(User user)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(InsertUser),
                    user.Guid,
                    user.CreatedAt,
                    user.UserName,
                    user.Email,
                    user.PhoneNumber,
                    user.Address,
                    user.PasswordHash
                );
        }

        public override AptMVC LoadApartmentById(int id)
        {
            var apartment = this.LoadRawApartmentById(id);
            var pictures = this.LoadApartmentPicturesByApartmentId(apartment.Id);
            var status = this.LoadRawApartmentStatusById(apartment.StatusId);
            var city = this.LoadRawCityById((int)apartment.CityId);
            var tags = this.LoadTagsByApartmentId(apartment.Id);
            var reviews = this.LoadApartmentReviewsByApartmentId(apartment.Id);

            apartment.Pictures = new List<AptPicMVC>(pictures);
            apartment.Status = status;
            apartment.City = city;
            apartment.Tags = new List<Tag>(tags);
            apartment.Reviews = new List<ApartmentReview>(reviews);

            return apartment;
        }

        public override IList<ApartmentReview> LoadApartmentReviewsByApartmentId(int id)
        {
            IList<ApartmentReview> apartmentReviews = new List<ApartmentReview>();

            var tblApartmentReviews = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentReviewsByApartmentId), id).Tables[0];
            foreach (DataRow row in tblApartmentReviews.Rows)
            {
                apartmentReviews.Add(
                    new ApartmentReview
                    {
                        Id = (int)(row[nameof(ApartmentReview.Id)]),
                        CreatedAt = (DateTime)row[nameof(ApartmentReview.CreatedAt)],
                        Guid = (Guid)row[nameof(ApartmentReview.Guid)],
                        ApartmentId = (int)row[nameof(ApartmentReview.ApartmentId)],
                        Details = !DBNull.Value.Equals(row[nameof(ApartmentReview.Details)]) ? (string)row[nameof(ApartmentReview.Details)] : null,
                        UserId = (int)row[nameof(ApartmentReview.UserId)],
                        Stars = (int)row[nameof(ApartmentReview.Stars)],
                    }
                );
            }

            foreach (var review in apartmentReviews)
            {
                review.User = this.LoadUserById(review.UserId);
            }

            return apartmentReviews;
        }

        public override AptOwnerMVC LoadApartmentOwnerById(int id)
        {
            var apartmentOwner = this.LoadRawApartmentOwnerById(id);
            var apartments = this.LoadApartmentsByOwnerId(apartmentOwner.Id);

            apartmentOwner.Apartments = new List<AptMVC>(apartments);

            return apartmentOwner;
        }

        public override IList<AptOwnerMVC> LoadApartmentOwners()
        {
            return this.LoadRawApartmentOwners();
        }

        public override AptPicMVC LoadApartmentPictureById(int id)
        {
            return this.LoadRawApartmentPictureById(id);
        }

        public override IList<AptPicMVC> LoadApartmentPictures()
        {
            return this.LoadRawApartmentPictures();
        }

        public override IList<AptPicMVC> LoadApartmentPicturesByApartmentId(int id)
        {
            IList<AptPicMVC> apartmentPictures = new List<AptPicMVC>();

            var tblApartmentPictures = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentPicturesByApartmentId), id).Tables[0];
            foreach (DataRow row in tblApartmentPictures.Rows)
            {
                apartmentPictures.Add(
                    new AptPicMVC
                    {
                        Id = (int)(row[nameof(AptPicMVC.Id)]),
                        Name = (string)row[nameof(AptPicMVC.Name)],
                        CreatedAt = (DateTime)row[nameof(AptPicMVC.CreatedAt)],
                        Guid = (Guid)row[nameof(AptPicMVC.Guid)],
                        ApartmentId = (int)row[nameof(AptPicMVC.ApartmentId)],
                        Base64Content = (string)row[nameof(AptPicMVC.Base64Content)],
                        IsRepresentative = (bool)row[nameof(AptPicMVC.IsRepresentative)],
                        Path = !DBNull.Value.Equals(row[nameof(AptPicMVC.Path)]) ? (string)row[nameof(AptPicMVC.Path)] : null,
                    }
                );
            }

            return apartmentPictures;
        }

        public override ApartmentReservation LoadApartmentReservationById(int id)
        {
            var reservation = this.LoadRawApartmentReservationById(id);
            if (reservation.UserId != null)
            {
                int userId = (int)reservation.UserId;
                var user = this.LoadRawUserById(userId);
                reservation.User = user;
            }
            else
            {
                var tempUser = new User
                {
                    Address = reservation.UserAddress,
                    Email = reservation.UserEmail,
                    UserName = reservation.UserName,
                    PhoneNumber = reservation.UserPhone
                };

                reservation.User = tempUser;
            }

            var apartment = this.LoadRawApartmentById(reservation.ApartmentId);

            reservation.Apartment = apartment;

            return reservation;
        }

        public override IList<ApartmentReservation> LoadApartmentReservations()
        {
            return this.LoadRawApartmentReservations();
        }

        public override ApartmentReview LoadApartmentReviewById(int id)
        {
            var review = this.LoadRawApartmentReviewById(id);
            var apartment = this.LoadRawApartmentById(review.ApartmentId);
            var user = this.LoadRawUserById(review.UserId);

            review.Apartment = apartment;
            review.User = user;

            return review;
        }

        public override IList<ApartmentReview> LoadApartmentReviews()
        {
            return this.LoadRawApartmentReviews();
        }

        public override IList<AptMVC> LoadApartments()
        {
            return this.LoadRawApartments();
        }

        public override IList<AptMVC> LoadApartments(params Predicate<AptMVC>[] filters)
        {
            var apartments = this.LoadRawApartments();
            var filteredApartments = new List<AptMVC>();
            foreach (var apartment in apartments)
            {
                var fullApartment = this.LoadApartmentById(apartment.Id);
                bool valid = true;
                foreach (var filter in filters)
                {
                    if (!filter(fullApartment))
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                    filteredApartments.Add(fullApartment);
            }
            return filteredApartments;
        }

        public override IList<AptMVC> LoadApartmentsByOwnerId(int id)
        {
            IList<AptMVC> apartments = new List<AptMVC>();

            var tblApartments = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentsByOwnerId), id).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new AptMVC
                    {
                        Id = (int)(row[nameof(AptMVC.Id)]),
                        Name = (string)row[nameof(AptMVC.Name)],
                        Address = (string)row[nameof(AptMVC.Address)],
                        CityId = (int)(row[nameof(AptMVC.CityId)]),
                        BeachDistance = (int)row[nameof(AptMVC.BeachDistance)],
                        CreatedAt = (DateTime)row[nameof(AptMVC.CreatedAt)],
                        Guid = (Guid)row[nameof(AptMVC.Guid)],
                        MaxAdults = (int)row[nameof(AptMVC.MaxAdults)],
                        MaxChildren = (int)row[nameof(AptMVC.MaxChildren)],
                        OwnerId = (int)row[nameof(AptMVC.OwnerId)],
                        Price = (decimal)row[nameof(AptMVC.Price)],
                        StatusId = (int)row[nameof(AptMVC.StatusId)],
                        TotalRooms = (int)row[nameof(AptMVC.TotalRooms)],
                        TypeId = (int)row[nameof(AptMVC.TypeId)]
                    }
                );
            }

            return apartments;
        }

        public override IList<ApartmentStatus> LoadApartmentStatus()
        {
            return this.LoadRawApartmentStatus();
        }

        public override ApartmentStatus LoadApartmentStatusById(int id)
        {
            return this.LoadRawApartmentStatusById(id);
        }

        public override IList<City> LoadCities()
        {
            return this.LoadRawCities();
        }

        public override City LoadCitiyById(int id)
        {
            return this.LoadRawCityById(id);
        }

        public override Tag LoadTagById(int id)
        {
            return this.LoadRawTagById(id);
        }

        public override TaggedApartment LoadTaggedApartmentById(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<TaggedApartment> LoadTaggedApartments()
        {
            throw new NotImplementedException();
        }

        public override IList<Tag> LoadTags()
        {
            return this.LoadRawTags();
        }

        public override IList<Tag> LoadTagsByApartmentId(int id)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(cs, nameof(LoadTagsByApartmentId), id).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)(row[nameof(Tag.Id)]),
                        Guid = (Guid)row[nameof(Tag.Guid)],
                        Name = (string)row[nameof(Tag.Name)],
                        CreatedAt = (DateTime)row[nameof(Tag.CreatedAt)],
                        NameEng = (string)row[nameof(Tag.NameEng)],
                        TypeId = (int)row[nameof(Tag.TypeId)]
                    }
                );
            }

            return tags;
        }

        public override IList<Tuple<Tag, int>> LoadTagsCounted()
        {
            var taggedApartments = this.LoadRawTaggedApartments();
            Dictionary<Tag, int> dict = new Dictionary<Tag, int>();

            foreach (var taggedApartment in taggedApartments)
            {
                var tempTag = this.LoadRawTagById(taggedApartment.TagId);
                if (!dict.Keys.Contains(tempTag))
                {
                    dict.Add(tempTag, 1);
                }
                else
                {
                    dict[tempTag]++;
                }
            }

            var allTags = this.LoadRawTags();
            foreach (var tag in allTags)
            {
                if (!dict.Keys.Contains(tag))
                {
                    dict.Add(tag, 0);
                }
            }

            return dict.Select(x => new Tuple<Tag, int>(x.Key, x.Value)).ToList();
        }

        public override TagType LoadTagTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<TagType> LoadTagTypes()
        {
            return this.LoadRawTagTypes();
        }

        public string LoadUserRoleByRoleId(int roleId)
        {
            IList<string> roles = new List<string>();

            var tblRoles = SqlHelper.ExecuteDataset(cs, nameof(LoadUserRoleByRoleId), roleId).Tables[0];

            foreach (DataRow row in tblRoles.Rows)
            {
                roles.Add((string)row["Name"]);
            }

            return roles[0];
        }

        public IList<string> LoadUserRolesByUserId(int userId)
        {
            IList<int> roleIds = new List<int>();
            var tblRoles = SqlHelper.ExecuteDataset(cs, nameof(LoadUserRolesByUserId), userId).Tables[0];

            foreach (DataRow row in tblRoles.Rows)
            {
                roleIds.Add((int)row["RoleId"]);
            }

            IList<string> roles = new List<string>();
            foreach (var roleId in roleIds)
            {
                roles.Add(this.LoadUserRoleByRoleId(roleId));
            }

            return roles;
        }

        public override User LoadUserById(int id)
        {
            var user = this.LoadRawUserById(id);
            user.Roles = this.LoadUserRolesByUserId(int.Parse(user.Id));

            return user;
        }

        public override IList<User> LoadUsers()
        {
            var users = this.LoadRawUsers();

            foreach (var user in users)
            {
                user.Roles = this.LoadUserRolesByUserId(int.Parse(user.Id));
            }

            return users;
        }

        public override void UpdateApartment(AptMVC apartment, IList<AptPicMVC> picturesToRemove)
        {
            int id = apartment.Id;
            var tagsFromDatabase = this.LoadTagsByApartmentId(apartment.Id);
            var currentTags = apartment.Tags;

            if (tagsFromDatabase.Count.Equals(0) || !currentTags.Equals(tagsFromDatabase))
            {
                IList<Tag> tagsToDelete = tagsFromDatabase.Except(currentTags).ToList();
                IList<Tag> tagsToAdd = currentTags.Except(tagsFromDatabase).ToList();

                foreach (Tag tag in tagsToDelete)
                {
                    this.DeleteTaggedApartment(new TaggedApartment { ApartmentId = apartment.Id, TagId = tag.Id });
                }

                foreach (Tag tag in tagsToAdd)
                {
                    this.InsertTaggedApartment(new TaggedApartment { Guid = Guid.NewGuid(), ApartmentId = apartment.Id, TagId = tag.Id });
                }
            }

            foreach (AptPicMVC picture in apartment.Pictures)
            {
                picture.ApartmentId = apartment.Id;
                if (picture.Id == 0)
                {
                    this.InsertApartmentPicture(picture);
                }
                else
                {
                    this.UpdateApartmentPicture(picture);
                }
            }

            foreach (AptPicMVC picture in picturesToRemove)
            {
                this.DeleteApartmentPicture(picture.Guid);
            }

            SqlHelper.ExecuteNonQuery(cs, nameof(UpdateApartment),
                apartment.Id,
                apartment.OwnerId,
                apartment.StatusId,
                apartment.CityId,
                apartment.Address,
                apartment.Name,
                apartment.NameEng,
                apartment.Price,
                apartment.MaxAdults,
                apartment.MaxChildren,
                apartment.TotalRooms,
                apartment.BeachDistance);
        }

        private void UpdateApartmentPicture(AptPicMVC picture)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(UpdateApartmentPicture), picture.Guid, picture.Name, picture.IsRepresentative);
        }

        public override IList<string> LoadApartmentNames()
        {
            IList<string> apartmentNames = new List<string>();

            var tblNames = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentNames)).Tables[0];

            foreach (DataRow row in tblNames.Rows)
            {
                apartmentNames.Add((string)row[nameof(AptMVC.NameEng)]);
            }

            return apartmentNames;
        }

        public override void GenerateApartmentReservation(ApartmentReservation apartmentReservation)
        {
            this.InsertApartmentReservation(apartmentReservation);
            this.UpdateApartmentAsReserved(apartmentReservation.ApartmentId);

        }

        private void UpdateApartmentAsReserved(int apartmentId)
        {
            SqlHelper.ExecuteNonQuery(cs, nameof(UpdateApartmentAsReserved), apartmentId);
        }
    }
}
