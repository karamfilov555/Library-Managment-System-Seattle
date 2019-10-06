using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly LMSContext _context;
        private readonly IUserService _userservice;
        private readonly UserManager<User> _userMng;

        public MembershipService(LMSContext context,
                                 IUserService userservice, 
                                 UserManager<User> userMng)
        {
            _context = context;
            _userservice = userservice;
            _userMng = userMng;
        }
        public async Task<User> MakeMember(string username)
        {
            var user = await _userservice.FindUserByUsernameAsync(username).ConfigureAwait(false);
            await _userMng.AddToRoleAsync(user, "Member").ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return user;
        }
    }
}
