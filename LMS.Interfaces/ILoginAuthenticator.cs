using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface ILoginAuthenticator
    {
        IUser GetCurrentUser();
        void SetCurrentUser(IUser _currentUser);
        User CheckUserCredetials(string username, string password);
        User CheckAdminCredetials(string username, string password);
        User CheckUsernameInAdminDb(string username);
        User CheckUsernameInUserDb(string username);
    }
}
