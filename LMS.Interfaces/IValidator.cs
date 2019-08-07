using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IValidator
    {
        bool IsNull(IUser currentUser);
        bool CommandNameIsLogin(string input);
        bool CommandNameIsRegister(string input);
        void IsAlreadyLoggedIn();
        void IsParametersCountIsValid(IList<string> parameteres, int count);
    }
}
