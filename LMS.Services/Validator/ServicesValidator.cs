using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data;
using System.Linq;
using LMS.Models;

namespace LMS.Services.Validator
{
    public class ServicesValidator : IServicesValidator
    {
        private readonly LMSContext _context;

        public ServicesValidator(LMSContext context)
        {
            _context = context;
        }
        public void CheckIfUsernameExists(string username)
        {
            if (_context.Users.Any(n => n.Username == username))
              throw new ArgumentException($"Username: {username} is taken.");
        }
        public bool CommandNameIsLogin(string input)
        {
            var command = input.Split()[0];
            if (command.ToLower() == "login")
                return true;
            return false;
        }
        public bool CommandNameIsRegister(string input)
        {
            var command = input.Split()[0];
            if (command.ToLower() == "register")
                return true;
            return false;
        }
        public bool IsNull(User user)
        {
            if (user == null)
                return true;
            return false;
        }
    }
}
