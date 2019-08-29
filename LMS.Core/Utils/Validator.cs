using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Utils
{
    public class Validator : IValidator
    {
        public Validator()
        {
        }
        public void CancelMembershipCountValidation(IList<string> parameteres)
        {
            if (parameteres.Count != 1)
                throw new ArgumentException("To Cancel your membership you should enter valid password!");
        }
        public void LoginParametersCountValidation(IList<string> parameteres)
        {
            if (parameteres.Count != 2)
                throw new ArgumentException($"To Login into the System you should enter Username and Password!");
        }
        public void RegisterParametersCountValidation(IList<string> parameteres)
        {
            if (parameteres.Count != 2)
                throw new ArgumentException("To Register into the System you should enter Username and Password!");
        }
        public void SearchByYearParametersCountValidation(IList<string> parameteres)
        {
            if (parameteres.Count != 1)
                throw new ArgumentException("Please, enter publication year of a book!");
        }

        public void CheckParametersCount(IList<string> parameters, int count)
        {
            if(parameters.Count != count)
                throw new ArgumentException("Invalid parameters count");
        }
        public void TryParseToInt(string year)
        {
            try
            {
                int.Parse(year);
            }
            catch (Exception)
            {
                throw new ArgumentException("Please, enter valid Number!");
            }
        }
        //public bool IsNull(IUser currentUser)
        //{
        //    if (currentUser == null)
        //        return true;
        //    return false;
        //}
        //public bool IsNull(IBook book)
        //{
        //    if (book == null)
        //        return true;
        //    return false;
        //}
        public bool CommandNameIsLogin(string input)
        {
            if (input.ToLower().Split()[0] == "login")
                return true;
            return false;
        }
        public bool CommandNameIsRegister(string input)
        {
            if (input.ToLower().Split()[0] == "register")
                return true;
            return false;
        }
    }
}
