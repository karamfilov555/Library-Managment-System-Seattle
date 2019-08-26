using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Contracts
{
    public interface IValidator
    {
        //bool IsNull(IUser currentUser);
        //bool IsNull(IBook book);
        bool CommandNameIsLogin(string input);
        bool CommandNameIsRegister(string input);
        void CancelMembershipCountValidation(IList<string> parameteres);
        void LoginParametersCountValidation(IList<string> parameteres);
        void RegisterParametersCountValidation(IList<string> parameteres);
        void SearchByYearParametersCountValidation(IList<string> parameteres);
        void TryParseToInt(string year);
    }
}
