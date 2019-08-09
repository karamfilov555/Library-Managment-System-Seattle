using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models
{
    public class User : IUser
    {
        private string username;
        private string password;
        
        public User(string username, string password)
        {
            this.Password = password;
            this.Username = username;
        }
        public string Username
        {
            get
            {
                return username;
            }
            private set
            {
                if (value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentException("Invalid username or password!");
                }
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            private set
            {
                if (value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentException("Invalid username or password!");
                }
                password = value;
            }
        }
    }
}
