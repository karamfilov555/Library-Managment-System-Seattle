using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IValidator
    {
        bool IsNull(IUser currentUser);
        bool IsNull(IBook book);
        bool CommandNameIsLogin(string input);
        bool CommandNameIsRegister(string input);
        void IsParametersCountIsValid(IList<string> parameteres, int count);
        void LoginParametersCountValidation(IList<string> parameteres);
        void RegisterParametersCountValidation(IList<string> parameteres);
    }
}
