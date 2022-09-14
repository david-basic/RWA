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
        
        public static DbContext Create()
        {
            var users = RepoFactory.GetRepo().LoadUsers();

            return new DbContext(users);
        }

        public void Dispose()
        {
        }
    }
}