using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Services.ModelProviders
{
    public class UserFactory : IUserFactory
    {
        private readonly IRoleServices _roleServices;
        public UserFactory(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        public User CreateUser(string username, string password, string roleName)
        {
            var role = _roleServices.ProvideRole(roleName);
            var user = new User(username, password, role);
            return user;
        }
    }
}