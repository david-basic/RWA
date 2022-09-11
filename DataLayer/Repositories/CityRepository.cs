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
    public class CityRepository
    {
        private readonly string _connectionString;
        public CityRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rwadb"].ConnectionString;
        }
        public List<Models.City> GetCities()
        {
            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetCities");

            var cityList = new List<Models.City>();
            cityList.Add(new Models.City { Id = 0, Name = "(odabir grada)" });

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var city = new Models.City
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Guid = Guid.Parse(row["Guid"].ToString()),
                    Name = row["Name"].ToString()
                };
                cityList.Add(city);
            }
            return cityList;
        }
    }
}
