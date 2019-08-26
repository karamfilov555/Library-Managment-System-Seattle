using LMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Validator
{
    public interface IServicesValidator
    {
        void CheckIfUsernameExists(string username);
        bool CommandNameIsLogin(string command);
        bool CommandNameIsRegister(string command);
        bool IsNull(User user);
    }
}
