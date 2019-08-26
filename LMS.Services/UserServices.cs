using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data;
using LMS.Data.Models;
using LMS.Services.Validator;
using System.Linq;

namespace LMS.Services
{
    public class UserServices : IUserServices
    {
        private readonly LMSContext _context;
        private readonly IServicesValidator _validator;
        // Validations TODO: ServiceValidator
        public UserServices(LMSContext context,
            IServicesValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public void AddUserToDb(User user)
        {
            _validator.CheckIfUsernameExists(user.Username);
            _context.User.Add(user);
            _context.SaveChanges();
        }
        public User CheckUserCredetials(string username, string password)
        {
            var user = _context.User.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                throw new ArgumentException("Wrong Credentials!");
            return user;
        }
    }
}
