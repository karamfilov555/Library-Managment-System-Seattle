using LMS.Contracts;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Utils
{
    public class Validator : IValidator
    {
        private const string ItemIsNull = "Null";
        private readonly ILoginAuthenticator _currentUser;
        public Validator(ILoginAuthenticator currentUser)
        {
            this._currentUser = currentUser;
        }
        public void IsParametersCountIsValid(IList<string> parameteres,int count)
        {
            if (parameteres.Count != count)
                throw new ArgumentException("Parameters count is not valid!");
        }
        public void IsAlreadyLoggedIn()
        {
            var currUsr = this._currentUser.GetCurrentUser();
            if (currUsr != null)
            {
                throw new ArgumentException("You are already LoggedIn!");
            }
        }
        public bool IsNull(IUser currentUser)
        {
            if (currentUser == null)
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
