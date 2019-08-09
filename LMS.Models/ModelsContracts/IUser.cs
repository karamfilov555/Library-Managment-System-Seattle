using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ModelsContracts
{
    public interface IUser
    {
        string Username { get; }
        string Password { get; }
    }
}
