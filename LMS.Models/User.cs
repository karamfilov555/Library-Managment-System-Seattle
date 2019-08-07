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
        private static string status;
        private static int books;
        private static bool isLogin;
        private static bool isAdmin;
        private static string currentUser;
        private static string currentPass;
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (value.Length < 3 || value.Length > 15)
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
                return this.password;
            }
            private set
            {
                if (value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentException("Invalid username or password!");
                }
                this.password = value;
            }
        }
    }
}
