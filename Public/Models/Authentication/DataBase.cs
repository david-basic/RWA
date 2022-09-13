using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public.Models.Authentication
{
    public class DataBase : IDisposable
    {
        public IList<User> Users { get; set; }
        public DataBase(IList<User> users)
        {
            Users = users;
        }
        
        public static DataBase Create()
        {
            
        }

        public void Dispose()
        {
        }

    }
}