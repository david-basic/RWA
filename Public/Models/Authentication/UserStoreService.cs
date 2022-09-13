using DataLayer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Public.Models.Authentication
{
    public class UserStoreService<TUser> : IUserStore<TUser>, IUserPasswordStore<TUser>, IUserRoleStore<TUser> where TUser : User
    {
        private readonly IList<TUser> _users;

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            user.AddRole(roleName);

            return Task.FromResult(0);
        }

        public Task CreateAsync(TUser user)
        {
            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;

            _users.Add(user);

            return Task.FromResult(true);
        }

        public Task DeleteAsync(TUser user)
        {
            return Task.FromResult(_users.Remove(user));
        }

        public void Dispose()
        {
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.UserName == userName));
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            return Task.FromResult(user.Roles);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.Password != null);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            user.RemoveRole(roleName);

            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.Password = passwordHash;

            return Task.FromResult(0);
        }

        public Task UpdateAsync(TUser user)
        {
            user.UpdatedTime = DateTime.Now;

            _users.Remove(user);
            _users.Add(user);

            return Task.FromResult(true);
        }
    }
}