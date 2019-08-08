using LMS.Models;
using System;
using System.Collections.Generic;

namespace LMS.Contracts
{
    public interface IUsersDataBase
    {
        void LoadUsersFromJson();
        User CheckUserCredetials(string username, string password);
        User CheckUsernameInUserDb(string username);
        void AddUserToDb(User user);
        void RemoveUserFromDb(string username);
        //void RemoveUserFromJsonDb(string userName);
    }
}
