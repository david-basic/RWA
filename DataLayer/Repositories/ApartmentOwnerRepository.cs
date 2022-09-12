using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ApartmentOwnerRepository
    {
        private readonly string _connectionString;
        public ApartmentOwnerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rwadb"].ConnectionString;
        }

        public List<Models.ApartmentOwner> GetApartmentOwners()
        {
            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetApartmentOwners");

            var ownerList = new List<Models.ApartmentOwner>();
            ownerList.Add(new Models.ApartmentOwner { Id = 0, Name = "(odabir vlasnika)" });

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var owner = new Models.ApartmentOwner
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString()
                };
                ownerList.Add(owner);
            }
            return ownerList;
        }
    }
}
