using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data;
using System.Linq;

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
            if (_context.User.Any(n => n.Username == username))
              throw new ArgumentException($"Username: {username} is taken.");
        }
    }
}
