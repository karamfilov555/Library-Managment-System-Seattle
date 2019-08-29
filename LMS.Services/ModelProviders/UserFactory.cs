using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Services.ModelProviders
{
    public class UserFactory : IUserFactory
    {
        private readonly IRoleServices _roleServices;
        private readonly IRecordFinesServices _recordServices;
        public UserFactory(IRoleServices roleServices,
                           IRecordFinesServices recordServices)
        {
            _roleServices = roleServices;
            _recordServices = recordServices;
        }
        public User CreateUser(string username, string password, string roleName)
        {
            var role = _roleServices.ProvideRole(roleName);
            var recordFines = _recordServices.ProvideRecord();
            var user = new User(username, password, role, recordFines);
            return user;
        }
    }
}