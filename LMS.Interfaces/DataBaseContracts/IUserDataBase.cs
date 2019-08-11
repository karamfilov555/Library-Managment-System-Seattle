using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts.DataBaseContracts
{
    public interface IUserDataBase
    {
        IList<User> ReadUsers();
        void WriteUsers(string jsonToOutput);
        void AddUserToJsonDB(string username, string password);
        void RemoveUserFromJsonDb(string userName);
    }
}
