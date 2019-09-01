using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;

namespace LMS.Services.Contracts
{
    public interface IUserServices
    {
        void AddUserToDb(User user);
        User CheckUserCredentials(string username, string password);
        void RemoveUserFromDb(User user);
        bool CheckIfUserExist(string username);
    }
}
