using DataLayer.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Factory
{
    public abstract class DataBaseRepoAbs : IRepo
    {
        public abstract User AuthUser(string username, string password);
        public abstract void DeleteApartment(AptMVC apartment);
        public abstract void DeleteApartmentPicture(Guid guid);
        public abstract void DeleteTag(int id);
        public abstract void DeleteTaggedApartment(int id);
        public abstract void DeleteUser(int id);
        public abstract void GenerateApartmentReservation(ApartmentReservation apartmentReservation);
        public abstract void InsertApartment(AptMVC apartment);
        public abstract void InsertApartmentOwner(AptOwnerMVC apartmentOwner);
        public abstract void InsertApartmentPicture(AptPicMVC apartmentPicture);
        public abstract void InsertApartmentReservation(ApartmentReservation apartmentReservation);
        public abstract void InsertApartmentReview(ApartmentReview apartmentReivew);
        public abstract void InsertApartmentStatus(ApartmentStatus apartmentStatus);
        public abstract void InsertCity(City city);
        public abstract void InsertTag(Tag tag);
        public abstract void InsertTaggedApartment(TaggedApartment taggedApartment);
        public abstract void InsertTagType(TagType tagType);
        public abstract void InsertUser(User user);
        public abstract AptMVC LoadApartmentById(int id);
        public abstract IList<string> LoadApartmentNames();
        public abstract AptOwnerMVC LoadApartmentOwnerById(int id);
        public abstract IList<AptOwnerMVC> LoadApartmentOwners();
        public abstract AptPicMVC LoadApartmentPictureById(int id);
        public abstract IList<AptPicMVC> LoadApartmentPictures();
        public abstract IList<AptPicMVC> LoadApartmentPicturesByApartmentId(int id);
        public abstract ApartmentReservation LoadApartmentReservationById(int id);
        public abstract IList<ApartmentReservation> LoadApartmentReservations();
        public abstract ApartmentReview LoadApartmentReviewById(int id);
        public abstract IList<ApartmentReview> LoadApartmentReviews();
        public abstract IList<ApartmentReview> LoadApartmentReviewsByApartmentId(int id);
        public abstract IList<AptMVC> LoadApartments();
        public abstract IList<AptMVC> LoadApartments(params Predicate<AptMVC>[] filters);
        public abstract IList<AptMVC> LoadApartmentsByOwnerId(int id);
        public abstract IList<ApartmentStatus> LoadApartmentStatus();
        public abstract ApartmentStatus LoadApartmentStatusById(int id);
        public abstract IList<City> LoadCities();
        public abstract City LoadCitiyById(int id);
        public abstract Tag LoadTagById(int id);
        public abstract TaggedApartment LoadTaggedApartmentById(int id);
        public abstract IList<TaggedApartment> LoadTaggedApartments();
        public abstract IList<Tag> LoadTags();
        public abstract IList<Tag> LoadTagsByApartmentId(int id);
        public abstract IList<Tuple<Tag, int>> LoadTagsCounted();
        public abstract TagType LoadTagTypeById(int id);
        public abstract IList<TagType> LoadTagTypes();
        public abstract User LoadUserById(int id);
        public abstract IList<User> LoadUsers();
        public abstract void UpdateApartment(AptMVC apartment, IList<AptPicMVC> picturesToRemove);

        public static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        internal IList<AptMVC> LoadRawApartments()
        {
            IList<AptMVC> apartments = new List<AptMVC>();

            var tblApartments = SqlHelper.ExecuteDataset(cs, nameof(LoadApartments)).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new AptMVC
                    {
                        Id = (int)(row[nameof(AptMVC.Id)]),
                        Name = (string)row[nameof(AptMVC.Name)],
                        NameEng = (string)row[nameof(AptMVC.NameEng)],
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

        internal IList<AptOwnerMVC> LoadRawApartmentOwners()
        {
            IList<AptOwnerMVC> apartmentsPictures = new List<AptOwnerMVC>();

            var tblApartmentOwners = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentOwners)).Tables[0];
            foreach (DataRow row in tblApartmentOwners.Rows)
            {
                apartmentsPictures.Add(
                    new AptOwnerMVC
                    {
                        Id = (int)(row[nameof(AptOwnerMVC.Id)]),
                        Name = (string)row[nameof(AptOwnerMVC.Name)],
                        CreatedAt = (DateTime)row[nameof(AptOwnerMVC.CreatedAt)],
                        Guid = (Guid)row[nameof(AptOwnerMVC.Guid)]
                    }
                );
            }

            return apartmentsPictures;
        }

        internal IList<AptPicMVC> LoadRawApartmentPictures()
        {
            IList<AptPicMVC> apartmentPictures = new List<AptPicMVC>();

            var tblApartmentPictures = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentPictures)).Tables[0];
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
                        Path = !DBNull.Value.Equals(row[nameof(AptPicMVC.Path)]) ? (string)row[nameof(AptPicMVC.Path)] : null
                    }
                );
            }

            return apartmentPictures;
        }

        internal IList<ApartmentReservation> LoadRawApartmentReservations()
        {
            IList<ApartmentReservation> apartmentReservations = new List<ApartmentReservation>();

            var tblApartmentReservations = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentReservations)).Tables[0];
            foreach (DataRow row in tblApartmentReservations.Rows)
            {
                apartmentReservations.Add(
                    new ApartmentReservation
                    {
                        Id = (int)(row[nameof(ApartmentReservation.Id)]),
                        CreatedAt = (DateTime)row[nameof(ApartmentReservation.CreatedAt)],
                        Guid = (Guid)row[nameof(ApartmentReservation.Guid)],
                        ApartmentId = (int)row[nameof(ApartmentReservation.ApartmentId)],
                        Details = (string)row[nameof(ApartmentReservation.Details)],
                        UserAddress = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserAddress)]) ? (string)row[nameof(ApartmentReservation.UserAddress)] : null,
                        UserEmail = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserEmail)]) ? (string)row[nameof(ApartmentReservation.UserEmail)] : null,
                        UserId = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserId)]) ? (int?)row[nameof(ApartmentReservation.UserId)] : null,
                        UserName = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserName)]) ? (string)row[nameof(ApartmentReservation.UserName)] : null,
                        UserPhone = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserPhone)]) ? (string)row[nameof(ApartmentReservation.UserPhone)] : null
                    }
                );
            }

            return apartmentReservations;
        }

        internal IList<ApartmentReview> LoadRawApartmentReviews()
        {
            IList<ApartmentReview> apartmentReviews = new List<ApartmentReview>();

            var tblApartmentReviews = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentReviews)).Tables[0];
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
                        Stars = (int)row[nameof(ApartmentReview.Stars)],
                        UserId = (int)row[nameof(ApartmentReview.UserId)]
                    }
                );
            }

            return apartmentReviews;
        }

        internal IList<ApartmentStatus> LoadRawApartmentStatus()
        {
            IList<ApartmentStatus> apartmentStatus = new List<ApartmentStatus>();

            var tblApartmentStatus = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentStatus)).Tables[0];
            foreach (DataRow row in tblApartmentStatus.Rows)
            {
                apartmentStatus.Add(
                    new ApartmentStatus
                    {
                        Id = (int)(row[nameof(ApartmentStatus.Id)]),
                        Guid = (Guid)row[nameof(ApartmentStatus.Guid)],
                        Name = (string)row[nameof(ApartmentStatus.Name)],
                        NameEng = (string)row[nameof(ApartmentStatus.NameEng)]
                    }
                );
            }

            return apartmentStatus;
        }

        internal IList<City> LoadRawCities()
        {
            IList<City> cities = new List<City>();

            var tblCites = SqlHelper.ExecuteDataset(cs, nameof(LoadCities)).Tables[0];
            foreach (DataRow row in tblCites.Rows)
            {
                cities.Add(
                    new City
                    {
                        Id = (int)(row[nameof(City.Id)]),
                        Guid = (Guid)row[nameof(City.Guid)],
                        Name = (string)row[nameof(City.Name)]
                    }
                );
            }

            return cities;
        }

        internal IList<Tag> LoadRawTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(cs, nameof(LoadTags)).Tables[0];
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

        internal IList<TaggedApartment> LoadRawTaggedApartments()
        {
            IList<TaggedApartment> taggedApartments = new List<TaggedApartment>();

            var tblTaggedApartments = SqlHelper.ExecuteDataset(cs, nameof(LoadTaggedApartments)).Tables[0];
            foreach (DataRow row in tblTaggedApartments.Rows)
            {
                taggedApartments.Add(
                    new TaggedApartment
                    {
                        Id = (int)(row[nameof(TaggedApartment.Id)]),
                        Guid = (Guid)row[nameof(TaggedApartment.Guid)],
                        ApartmentId = (int)row[nameof(TaggedApartment.ApartmentId)],
                        TagId = (int)row[nameof(TaggedApartment.TagId)]
                    }
                );
            }

            return taggedApartments;
        }

        internal IList<TagType> LoadRawTagTypes()
        {
            IList<TagType> tagTypes = new List<TagType>();

            var tblTagTypes = SqlHelper.ExecuteDataset(cs, nameof(LoadTagTypes)).Tables[0];
            foreach (DataRow row in tblTagTypes.Rows)
            {
                tagTypes.Add(
                    new TagType
                    {
                        Id = (int)(row[nameof(TagType.Id)]),
                        Guid = (Guid)row[nameof(TagType.Guid)],
                        Name = (string)row[nameof(TagType.Name)],
                        NameEng = (string)row[nameof(TagType.NameEng)]
                    }
                );
            }

            return tagTypes;
        }

        internal IList<User> LoadRawUsers()
        {
            IList<User> users = new List<User>();

            var tblUsers = SqlHelper.ExecuteDataset(cs, nameof(LoadUsers)).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        Id = ((int)(row[nameof(User.Id)])).ToString(),
                        Guid = (Guid)row[nameof(User.Guid)],
                        Address = (string)row[nameof(User.Address)],
                        CreatedAt = (DateTime)row[nameof(User.CreatedAt)],
                        Email = (string)row[nameof(User.Email)],
                        PasswordHash = !DBNull.Value.Equals(row[nameof(User.PasswordHash)]) ? (string)row[nameof(User.PasswordHash)] : null,
                        Password = !DBNull.Value.Equals(row[nameof(User.PasswordHash)]) ? (string)row[nameof(User.PasswordHash)] : null,
                        PhoneNumber = (string)row[nameof(User.PhoneNumber)],
                        UserName = (string)row[nameof(User.Email)],
                        FullName = (string)row[nameof(User.UserName)],
                        Roles = new List<string>()
                    }
                );
            }
            return users;
        }

        internal AptMVC LoadRawApartmentById(int id)
        {
            var tblApartments = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentById), id).Tables[0];
            if (tblApartments.Rows.Count == 0) return null;

            DataRow row = tblApartments.Rows[0];
            return new AptMVC
            {
                Id = (int)(row[nameof(AptMVC.Id)]),
                Name = (string)row[nameof(AptMVC.Name)],
                NameEng = (string)row[nameof(AptMVC.NameEng)],
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
            };
        }

        internal AptOwnerMVC LoadRawApartmentOwnerById(int id)
        {
            var tblApartmentOwners = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentOwnerById), id).Tables[0];
            if (tblApartmentOwners.Rows.Count == 0) return null;

            DataRow row = tblApartmentOwners.Rows[0];
            return new AptOwnerMVC
            {
                Id = (int)(row[nameof(AptOwnerMVC.Id)]),
                Name = (string)row[nameof(AptOwnerMVC.Name)],
                CreatedAt = (DateTime)row[nameof(AptOwnerMVC.CreatedAt)],
                Guid = (Guid)row[nameof(AptOwnerMVC.Guid)]
            };
        }

        internal AptPicMVC LoadRawApartmentPictureById(int id)
        {
            var tblApartmentPictures = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentPictureById), id).Tables[0];
            if (tblApartmentPictures.Rows.Count == 0) return null;

            DataRow row = tblApartmentPictures.Rows[0];
            return new AptPicMVC
            {
                Id = (int)(row[nameof(AptPicMVC.Id)]),
                Name = (string)row[nameof(AptPicMVC.Name)],
                CreatedAt = (DateTime)row[nameof(AptPicMVC.CreatedAt)],
                Guid = (Guid)row[nameof(AptPicMVC.Guid)],
                ApartmentId = (int)row[nameof(AptPicMVC.ApartmentId)],
                Base64Content = (string)row[nameof(AptPicMVC.Base64Content)],
                IsRepresentative = (bool)row[nameof(AptPicMVC.IsRepresentative)],
                Path = !DBNull.Value.Equals(row[nameof(AptPicMVC.Path)]) ? (string)row[nameof(AptPicMVC.Path)] : null
            };
        }

        internal ApartmentReservation LoadRawApartmentReservationById(int id)
        {
            var tblApartmentReservations = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentReservationById), id).Tables[0];
            if (tblApartmentReservations.Rows.Count == 0) return null;

            DataRow row = tblApartmentReservations.Rows[0];
            return new ApartmentReservation
            {
                Id = (int)(row[nameof(ApartmentReservation.Id)]),
                CreatedAt = (DateTime)row[nameof(ApartmentReservation.CreatedAt)],
                Guid = (Guid)row[nameof(ApartmentReservation.Guid)],
                ApartmentId = (int)row[nameof(ApartmentReservation.ApartmentId)],
                Details = (string)row[nameof(ApartmentReservation.Details)],
                UserAddress = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserAddress)]) ? (string)row[nameof(ApartmentReservation.UserAddress)] : null,
                UserEmail = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserEmail)]) ? (string)row[nameof(ApartmentReservation.UserEmail)] : null,
                UserId = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserId)]) ? (int?)row[nameof(ApartmentReservation.UserId)] : null,
                UserName = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserName)]) ? (string)row[nameof(ApartmentReservation.UserName)] : null,
                UserPhone = !DBNull.Value.Equals(row[nameof(ApartmentReservation.UserPhone)]) ? (string)row[nameof(ApartmentReservation.UserPhone)] : null
            };
        }

        internal ApartmentReview LoadRawApartmentReviewById(int id)
        {
            var tblApartmentReviews = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentReviewById), id).Tables[0];
            if (tblApartmentReviews.Rows.Count == 0) return null;

            DataRow row = tblApartmentReviews.Rows[0];
            return new ApartmentReview
            {
                Id = (int)(row[nameof(ApartmentReview.Id)]),
                CreatedAt = (DateTime)row[nameof(ApartmentReview.CreatedAt)],
                Guid = (Guid)row[nameof(ApartmentReview.Guid)],
                ApartmentId = (int)row[nameof(ApartmentReview.ApartmentId)],
                Details = !DBNull.Value.Equals(row[nameof(ApartmentReview.Details)]) ? (string)row[nameof(ApartmentReview.Details)] : null,
                Stars = (int)row[nameof(ApartmentReview.Stars)],
                UserId = (int)row[nameof(ApartmentReview.UserId)]
            };
        }

        internal ApartmentStatus LoadRawApartmentStatusById(int id)
        {
            var tblApartmentStatus = SqlHelper.ExecuteDataset(cs, nameof(LoadApartmentStatusById), id).Tables[0];
            if (tblApartmentStatus.Rows.Count == 0) return null;

            DataRow row = tblApartmentStatus.Rows[0];
            return new ApartmentStatus
            {
                Id = (int)(row[nameof(ApartmentStatus.Id)]),
                Guid = (Guid)row[nameof(ApartmentStatus.Guid)],
                Name = (string)row[nameof(ApartmentStatus.Name)],
                NameEng = (string)row[nameof(ApartmentStatus.NameEng)]
            };
        }

        internal City LoadRawCityById(int id)
        {
            var tblCities = SqlHelper.ExecuteDataset(cs, nameof(LoadCitiyById), id).Tables[0];
            if (tblCities.Rows.Count == 0) return null;

            DataRow row = tblCities.Rows[0];
            return new City
            {
                Id = (int)(row[nameof(City.Id)]),
                Guid = (Guid)row[nameof(City.Guid)],
                Name = (string)row[nameof(City.Name)]
            };
        }

        internal Tag LoadRawTagById(int id)
        {
            var tblTags = SqlHelper.ExecuteDataset(cs, nameof(LoadTagById), id).Tables[0];
            if (tblTags.Rows.Count == 0) return null;

            DataRow row = tblTags.Rows[0];
            return new Tag
            {
                Id = (int)(row[nameof(Tag.Id)]),
                Guid = (Guid)row[nameof(Tag.Guid)],
                Name = (string)row[nameof(Tag.Name)],
                CreatedAt = (DateTime)row[nameof(Tag.CreatedAt)],
                NameEng = (string)row[nameof(Tag.NameEng)],
                TypeId = (int)row[nameof(Tag.TypeId)]
            };
        }

        internal TaggedApartment LoadRawTaggedApartmentsById(int id)
        {
            var tblTaggedApartmets = SqlHelper.ExecuteDataset(cs, nameof(LoadTaggedApartmentById), id).Tables[0];
            if (tblTaggedApartmets.Rows.Count == 0) return null;

            DataRow row = tblTaggedApartmets.Rows[0];
            return new TaggedApartment
            {
                Id = (int)(row[nameof(TaggedApartment.Id)]),
                Guid = (Guid)row[nameof(TaggedApartment.Guid)],
                ApartmentId = (int)row[nameof(TaggedApartment.ApartmentId)],
                TagId = (int)row[nameof(TaggedApartment.TagId)]
            };
        }

        internal TagType LoadRawTagTypesById(int id)
        {
            var tblTagTypes = SqlHelper.ExecuteDataset(cs, nameof(LoadTagTypeById), id).Tables[0];
            if (tblTagTypes.Rows.Count == 0) return null;

            DataRow row = tblTagTypes.Rows[0];
            return new TagType
            {
                Id = (int)(row[nameof(TagType.Id)]),
                Guid = (Guid)row[nameof(TagType.Guid)],
                Name = (string)row[nameof(TagType.Name)],
                NameEng = (string)row[nameof(TagType.NameEng)]
            };
        }

        internal User LoadRawUserById(int id)
        {
            var tblUsers = SqlHelper.ExecuteDataset(cs, nameof(LoadUserById), id).Tables[0];
            if (tblUsers.Rows.Count == 0) return null;

            DataRow row = tblUsers.Rows[0];
            return new User
            {
                Id = ((int)(row[nameof(User.Id)])).ToString(),
                Guid = (Guid)row[nameof(User.Guid)],
                Address = (string)row[nameof(User.Address)],
                CreatedAt = (DateTime)row[nameof(User.CreatedAt)],
                Email = (string)row[nameof(User.Email)],
                PasswordHash = !DBNull.Value.Equals(row[nameof(User.PasswordHash)]) ? (string)row[nameof(User.PasswordHash)] : null,
                Password = !DBNull.Value.Equals(row[nameof(User.PasswordHash)]) ? (string)row[nameof(User.PasswordHash)] : null,
                PhoneNumber = (string)row[nameof(User.PhoneNumber)],
                UserName = (string)row[nameof(User.Email)],
                FullName = (string)row[nameof(User.UserName)],
                Roles = new List<string>()
            };
        }
    }
}
