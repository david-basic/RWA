using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Repositories.Factory;
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
            //var users = RepoFactory.GetRepo().LoadUsers();
            var users = new List<User>
            {
                new User()
                {
                    CreatedAt = DateTime.Now,
                    UserName = "david@admin.hr",
                    FullName = "David Bašić",
                    Email = "david@admin.hr",
                    Password = "1234",
                    Roles = new List<string>() { "Admin" }
                }
            };

            return new DataBase(users);
        }

        public void Dispose()
        {
        }

    }
}