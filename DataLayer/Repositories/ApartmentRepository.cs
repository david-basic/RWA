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
    }
}
