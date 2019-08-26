using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data.Models;

namespace LMS.Services.Contracts
{
    public interface IUserServices
    {
        void AddUserToDb(User user);
        User CheckUserCredetials(string username, string password);
    }
}
