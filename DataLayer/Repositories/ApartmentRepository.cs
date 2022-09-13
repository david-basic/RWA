using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ApartmentRepository
    {
        private readonly string _connectionString;
        public ApartmentRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rwadb"].ConnectionString;
        }

        public List<Models.Apartment> GetApartments(int? statusId, int? cityId, int? order)
        {
            var commandParameters = new List<SqlParameter>();

            if (statusId.HasValue && statusId.Value != 0)
            {
                commandParameters.Add(new SqlParameter("@statusId", statusId));
            }

            if (cityId.HasValue && cityId.Value != 0)
            {
                commandParameters.Add(new SqlParameter("@cityId", cityId));
            }

            if (order.HasValue && order.Value != 0)
            {
                commandParameters.Add(new SqlParameter("@order", order));
            }

            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetApartments", commandParameters.ToArray());

            var apList = new List<Models.Apartment>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var ap = new Models.Apartment
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Guid = Guid.Parse(row["Guid"].ToString()),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    DeletedAt = row["DeletedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["DeletedAt"]) : null,
                    OwnerId = Convert.ToInt32(row["OwnerId"]),
                    OwnerName = row["OwnerName"].ToString(),
                    TypeId = Convert.ToInt32(row["TypeId"]),
                    StatusId = Convert.ToInt32(row["StatusId"]),
                    StatusName = row["StatusName"].ToString(),
                    CityId = row["CityId"] != DBNull.Value ? (int?)Convert.ToInt32(row["CityId"]) : null,
                    CityName = row["CityName"]?.ToString(),
                    Address = row["Address"].ToString(),
                    Name = row["Name"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    MaxAdults = row["MaxAdults"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaxAdults"]) : null,
                    MaxChildren = row["MaxChildren"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaxChildren"]) : null,
                    TotalRooms = row["TotalRooms"] != DBNull.Value ? (int?)Convert.ToInt32(row["TotalRooms"]) : null,
                    BeachDistance = row["BeachDistance"] != DBNull.Value ? (int?)Convert.ToInt32(row["BeachDistance"]) : null
                };
                apList.Add(ap);
            }
            return apList;
        }
        public Models.Apartment GetApartment(int id)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@id", id));

            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetApartment", commandParameters.ToArray());

            var row = ds.Tables[0].Rows[0];

            var ap = new Models.Apartment
            {
                Id = Convert.ToInt32(row["ID"]),
                Guid = Guid.Parse(row["Guid"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                DeletedAt = row["DeletedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["DeletedAt"]) : null,
                OwnerId = Convert.ToInt32(row["OwnerId"]),
                OwnerName = row["OwnerName"].ToString(),
                TypeId = Convert.ToInt32(row["TypeId"]),
                StatusId = Convert.ToInt32(row["StatusId"]),
                StatusName = row["StatusName"].ToString(),
                CityId = row["CityId"] != DBNull.Value ? (int?)Convert.ToInt32(row["CityId"]) : null,
                CityName = row["CityName"]?.ToString(),
                Address = row["Address"].ToString(),
                Name = row["Name"].ToString(),
                Price = Convert.ToDecimal(row["Price"]),
                MaxAdults = row["MaxAdults"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaxAdults"]) : null,
                MaxChildren = row["MaxChildren"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaxChildren"]) : null,
                TotalRooms = row["TotalRooms"] != DBNull.Value ? (int?)Convert.ToInt32(row["TotalRooms"]) : null,
                BeachDistance = row["BeachDistance"] != DBNull.Value ? (int?)Convert.ToInt32(row["BeachDistance"]) : null
            };
            return ap;
        }

        public void CreateApartment(Models.Apartment apartment)
        {
            var commandParameters = new List<SqlParameter>
            {
                new SqlParameter("@guid", apartment.Guid),
                new SqlParameter("@ownerId", apartment.OwnerId),
                new SqlParameter("@typeId", apartment.TypeId),
                new SqlParameter("@statusId", apartment.StatusId),
                new SqlParameter("@cityId", apartment.CityId),
                new SqlParameter("@address", apartment.Address),
                new SqlParameter("@name", apartment.Name),
                new SqlParameter("@price", apartment.Price),
                new SqlParameter("@maxAdults", apartment.MaxAdults),
                new SqlParameter("@maxChildren", apartment.MaxChildren),
                new SqlParameter("@totalRooms", apartment.TotalRooms),
                new SqlParameter("@beachDistance", apartment.BeachDistance)
            };

            DataTable dtTags = new DataTable();
            dtTags.Columns.AddRange(
                new DataColumn[1] {
                    new DataColumn("Key", typeof(int))
                });

            foreach (var tag in apartment.Tags)
            {
                dtTags.Rows.Add(tag.Id);
            }

            commandParameters.Add(new SqlParameter("@tags", dtTags));

            DataTable dtPics = new DataTable();
            dtPics.Columns.AddRange(
                new DataColumn[] {
                    new DataColumn("Id", typeof(int)),
                    new DataColumn("Path", typeof(string)),
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("IsRepresentative", typeof(bool)),
                    new DataColumn("DoDelete", typeof(bool))
                });

            foreach (var apartmentPicture in apartment.ApartmentPictures)
            {
                if (!apartmentPicture.DoDelete)
                {
                    dtPics.Rows.Add(
                    apartmentPicture.Id,
                    apartmentPicture.Path,
                    apartmentPicture.Name,
                    apartmentPicture.IsRepresentative,
                    apartmentPicture.DoDelete);
                }
            }
            commandParameters.Add(new SqlParameter("@pictures", dtPics));

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "dbo.CreateApartment", commandParameters.ToArray());
        }

        public List<Models.Tag> GetApartmentTags(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));

            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetApartmentTags", commandParameters.ToArray());

            var tags = new List<Models.Tag>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tags.Add(new Models.Tag
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                });
            }
            return tags;
        }

        public void DeleteApartment(int id)
        {
            var commandParameters = new List<SqlParameter>();

            commandParameters.Add(new SqlParameter("@id", id));

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "dbo.DeleteApartment", commandParameters.ToArray());
        }

        public void UpdateApartment(Models.Apartment apartment)
        {
            var commandParameters = new List<SqlParameter>
            {
                new SqlParameter("@id", apartment.Id),
                new SqlParameter("@guid", apartment.Guid),
                new SqlParameter("@ownerId", apartment.OwnerId),
                new SqlParameter("@typeId", apartment.TypeId),
                new SqlParameter("@statusId", apartment.StatusId),
                new SqlParameter("@cityId", apartment.CityId),
                new SqlParameter("@address", apartment.Address),
                new SqlParameter("@name", apartment.Name),
                new SqlParameter("@price", apartment.Price),
                new SqlParameter("@maxAdults", apartment.MaxAdults),
                new SqlParameter("@maxChildren", apartment.MaxChildren),
                new SqlParameter("@totalRooms", apartment.TotalRooms),
                new SqlParameter("@beachDistance", apartment.BeachDistance)
            };

            DataTable dtTags = new DataTable();
            dtTags.Columns.AddRange(
                new DataColumn[1] {
                    new DataColumn("Key", typeof(int))
                });

            foreach (var tag in apartment.Tags)
            {
                dtTags.Rows.Add(tag.Id);
            }

            commandParameters.Add(new SqlParameter("@tags", dtTags));

            DataTable dtPics = new DataTable();

            dtPics.Columns.AddRange(
                new DataColumn[] {
                    new DataColumn("Id", typeof(int)),
                    new DataColumn("Path", typeof(string)),
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("IsRepresentative", typeof(bool)),
                    new DataColumn("DoDelete", typeof(bool))
                });

            foreach (var apartmentPicture in apartment.ApartmentPictures)
            {
                if (!apartmentPicture.DoDelete)
                {
                    dtPics.Rows.Add(
                    apartmentPicture.Id,
                    apartmentPicture.Path,
                    apartmentPicture.Name,
                    apartmentPicture.IsRepresentative,
                    apartmentPicture.DoDelete);
                }
            }
            commandParameters.Add(new SqlParameter("@pictures", dtPics));

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "dbo.UpdateApartment", commandParameters.ToArray());
        }

        public List<Models.ApartmentPicture> GetApartmentPictures(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));

            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetApartmentPictures", commandParameters.ToArray());
            var pics = new List<Models.ApartmentPicture>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                pics.Add(new Models.ApartmentPicture
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Path = row["Path"].ToString(),
                    Name = row["Name"].ToString(),
                    IsRepresentative = bool.Parse(row["IsRepresentative"].ToString())
                });
            }
            return pics;
        }
    }
}
