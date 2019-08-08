using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.DataBase
{
    public class UsersDataBase : IUsersDataBase
    {
        private readonly IList<User> users = new List<User>();
        private readonly IJson _json;
        public UsersDataBase(IJson json)
        {
            this._json = json;
        }
        public void LoadUsersFromJson()
        {
            var existingUsers = _json.ReadUsers();

            foreach (var user in existingUsers)
            {
                users.Add(user);
            }
        }
        public User CheckUserCredetials(string username, string password)
        {
            var user = this.users.FirstOrDefault(u => u.Username == username
                                                           && u.Password == password);
            return user;
        }
        public User CheckUsernameInUserDb(string username)
        {
            var user = this.users.FirstOrDefault(u => u.Username == username);
            return user;
        }
        public void AddUserToDb(User user)
        {
            this.users.Add(user);
            this._json.AddUserToJsonDB(user.Username,user.Password);
        }
        public void RemoveUserFromDb(string username)
        {
            var userToBeDeleted = this.users.FirstOrDefault(x => x.Username == username);
            this.users.Remove(userToBeDeleted);
            this._json.RemoveUserFromJsonDb(username);
        }
        //public void RemoveUserFromJsonDb(string userName)
        //{
        //    var existingUsers = this._json.ReadUsers();
        //    var user = existingUsers.FirstOrDefault(x => x.Username == userName);
        //    existingUsers.Remove(user);
        //    var jsonToOutput = JsonConvert.SerializeObject(existingUsers, Formatting.Indented);
        //    this._json.WriteUsers(jsonToOutput);
        //}
    }
}
