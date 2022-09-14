using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Repositories.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public.Models.Authentication
{
    public class DbContext : IDisposable
    {
        public IList<User> Users { get; set; }

        public DbContext(IList<User> users)
        {
            Users = users;
        }
        public void Dispose()
        {
        }
        
        public static DbContext Create()
        {
            //var users = new List<User>
            //{
            //    new User()
            //    {
            //        CreatedAt = DateTime.Now,
            //        UserName = "david@admin.hr",
            //        FullName = "David Basic",
            //        Email = "david@admin.hr",
            //        Password = "1234",
            //        Roles = new List<string>() { "Admin" }
            //    }
            //};

            var users = RepoFactory.GetRepo().LoadUsers();

            return new DbContext(users);
        }

    }
}