using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IAdminsDataBase
    {
        void LoadAdminsFromJson();
        User CheckAdminCredentials(string username, string password);
        User CheckUsernameInAdminDb(string username);
    }
}
