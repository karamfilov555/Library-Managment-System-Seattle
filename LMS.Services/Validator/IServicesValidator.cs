using LMS.Models;

namespace LMS.Services.Validator
{
    public interface IServicesValidator
    {
        void CheckIfUsernameExists(string username);
        bool CommandNameIsLogin(string command);
        bool CommandNameIsRegister(string command);
        bool IsNull(User user);
    }
}
