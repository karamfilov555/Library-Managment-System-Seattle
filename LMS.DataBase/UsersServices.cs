using LMS.Contracts;
using LMS.Contracts.DataBaseContracts;
using LMS.Models.ModelsContracts;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IList<IUser> users = new List<IUser>();
        private readonly IUserDataBase _userDb;
        public UsersServices(IUserDataBase userDb)
        {
            _userDb = userDb;
        }
        public void LoadUsersFromJson()
        {
            var existingUsers = _userDb.ReadUsers();

            foreach (var user in existingUsers)
            {
                users.Add(user);
            }
        }
        public IUser CheckUserCredetials(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username
                                                           && u.Password == password);
            return user;
        }
        public IUser CheckUsernameInUserDb(string username)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            return user;
        }
        public void AddUserToDb(IUser user)
        {
            users.Add(user);
            _userDb.AddUserToJsonDB(user.Username,user.Password);
        }
        public void RemoveUserFromDb(string username)
        {
            var userToBeDeleted = users.FirstOrDefault(x => x.Username == username);
            users.Remove(userToBeDeleted);
            _userDb.RemoveUserFromJsonDb(username);
        }
    }
}
