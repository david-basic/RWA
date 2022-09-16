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
    public class TagRepository
    {
        private readonly string _connectionString;
        public TagRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rwadb"].ConnectionString;
        }
        public List<Models.Tag> GetTags(bool forTagListAspx)
        {
            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "dbo.GetTags");

            var tagList = new List<Models.Tag>();
            if (!forTagListAspx)
            {
                tagList.Add(new Models.Tag { Id = 0, Name = "(odabir taga)" }); 
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var tag = new Models.Tag
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                };
                tagList.Add(tag);
            }
            return tagList;
        }
    }
}
