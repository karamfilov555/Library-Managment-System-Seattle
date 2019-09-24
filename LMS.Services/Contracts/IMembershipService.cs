using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IMembershipService
    {
        Task<User> MakeMember(string username);
    }
}
