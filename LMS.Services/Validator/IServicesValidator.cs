using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Validator
{
    public interface IServicesValidator
    {
        void CheckIfUsernameExists(string username);
    }
}
