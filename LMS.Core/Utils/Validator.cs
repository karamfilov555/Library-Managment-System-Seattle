using LMS.Contracts;
using LMS.Models.ModelsContracts;
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
        public void IsParametersCountIsValid(IList<string> parameteres,int count)
        {
            if (parameteres.Count != count)
                throw new ArgumentException("Parameters count is not valid!");
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
        public void CheckOutBookParamsValidation(IList<string> parameteres)
        {
            if (parameteres.Count != 1)
                throw new ArgumentException("To Check-out book you should enter book's title!");
        }

        public bool IsNull(IUser currentUser)
        {
            if (currentUser == null)
                return true;
            return false;
        }
        public bool IsNull(IBook book)
        {
            if (book == null)
                return true;
            return false;
        }
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
