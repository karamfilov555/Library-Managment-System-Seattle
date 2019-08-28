using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace LMS.Services
{
    public class DataBaseLoader : IDataBaseLoader
    {
        private readonly LMSContext _context;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        public DataBaseLoader(LMSContext context,
                              IUserServices userServices,
                              IRoleServices roleServices)
        {
            _context = context;
            _userServices = userServices;
            _roleServices = roleServices;
        }
        public void SeedDataBase()
        {
            LoadRoles();
            LoadUsers();
        }
        public void LoadUsers()
        {
            var usersAsJson = File.ReadAllText(@"..\..\..\..\LMS.Data\Json\Users.json");
            var users = JsonConvert.DeserializeObject<User[]>(usersAsJson);
            var sqlCommand = new StringBuilder();

            foreach (var user in users)
            {
                if (!_userServices.CheckIfUserExist(user.Username))
                {
                    sqlCommand.AppendLine($@"
                    
                    SELECT *  FROM dbo.Users u
                                WHERE u.Id = {user.Id}
                        BEGIN
                        INSERT INTO dbo.Users
                             (Username, Password, RoleId) 
                        VALUES ('{user.Username}', '{user.Password}','{user.RoleId}')
                    END");
                }
            }
            if (sqlCommand.Length!=0)
            _context.Database.ExecuteSqlCommand(sqlCommand.ToString());
        }
        public void LoadRoles()
        {
            var rolesAsJson = File.ReadAllText(@"..\..\..\..\LMS.Data\Json\Roles.json");
            var roles = JsonConvert.DeserializeObject<Role[]>(rolesAsJson);
            var sqlCommand = new StringBuilder();

            foreach (var role in roles)
            {
                if (!_roleServices.CheckIfRoleExist(role.Name))
                {
                    sqlCommand.AppendLine($@"
                    
                      SELECT * FROM dbo.Roles r
                             WHERE r.Id = {role.Id}
                        BEGIN
                        INSERT INTO dbo.Roles
                             (Name) 
                        VALUES ('{role.Name}')
                      END ");
                }
            }
            if (sqlCommand.Length!=0)
            _context.Database.ExecuteSqlCommand(sqlCommand.ToString());
        }
    }
}
