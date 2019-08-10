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
                return this.username;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentException("Invalid username or password!");
                }
                this.username = value;
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
                if (string.IsNullOrWhiteSpace(value)||
                    value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentException("Invalid username or password!");
                }
                password = value;
            }
        }
    }
}
