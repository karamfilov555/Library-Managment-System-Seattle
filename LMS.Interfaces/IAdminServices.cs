using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IAdminServices
    {
        void LoadAdminsFromJson();
        User CheckAdminCredentials(string username, string password);
        User CheckUsernameInAdminDb(string username);
        bool CheckIUserInAdminDb(IUser user);
        void RemoveAdminFromDb(string username);
    }
}
