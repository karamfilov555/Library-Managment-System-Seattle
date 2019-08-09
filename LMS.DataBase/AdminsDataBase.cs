﻿using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class AdminsDataBase : IAdminsDataBase
    {
        private readonly IJson _json;
        private readonly IList<User> admins = new List<User>();
        public AdminsDataBase(IJson json)
        {
            _json = json;
        }
        public void LoadAdminsFromJson()
        {
            var existingAdmins = _json.ReadAdmins();

            foreach (var admin in existingAdmins)
            {
                admins.Add(admin);
            }
        }
        public User CheckAdminCredentials(string username, string password)
        {
            var admin = admins.FirstOrDefault(u => u.Username == username
                                                            && u.Password == password);
            return admin;
        }
        public User CheckUsernameInAdminDb(string username)
        {
            var admin = admins.FirstOrDefault(u => u.Username == username);
            return admin;
        }
        public bool CheckIUserInAdminDb(IUser user)
        {
            var admin = admins.FirstOrDefault(u => u == user);
            if (admin != null)
                return true;
            return false;
        }
        public void RemoveAdminFromDb(string username)
        {
            var adminToBeDeleted = admins.FirstOrDefault(x => x.Username == username);
            admins.Remove(adminToBeDeleted);
            _json.RemoveAdminFromJsonDb(username);
        }
    }
}
