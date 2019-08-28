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
        public DataBaseLoader(LMSContext context)
        {
            _context = context;
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
                sqlCommand.AppendLine($@"
                    
                    IF NOT EXISTS 
                            (SELECT *  FROM dbo.Users u
                                WHERE u.Id = {user.Id})
                        BEGIN
                        INSERT INTO dbo.Users
                             (Username, Password, RoleId) 
                        VALUES ('{user.Username}', '{user.Password}','{user.RoleId}')
                    END 
                    ELSE
                        BEGIN
                        UPDATE dbo.Users
                        SET Username = '{user.Username}', Password = '{user.Password}',
                            RoleId = '{user.RoleId}'
                        WHERE dbo.Users.Id = {user.Id}
                    END");
            }
            _context.Database.ExecuteSqlCommand(sqlCommand.ToString());
        }
        public void LoadRoles()
        {
            var rolesAsJson = File.ReadAllText(@"..\..\..\..\LMS.Data\Json\Roles.json");

            var roles = JsonConvert.DeserializeObject<Role[]>(rolesAsJson);

            var sqlCommand = new StringBuilder();

            foreach (var role in roles)
            {
                sqlCommand.AppendLine($@"
                    
                      IF NOT EXISTS 
                            (SELECT * FROM dbo.Roles r
                             WHERE r.Id = {role.Id})
                        BEGIN
                        INSERT INTO dbo.Roles
                             (Name) 
                        VALUES ('{role.Name}')
                      END
                      ELSE
                        BEGIN
                        UPDATE dbo.Roles
                        SET Name = '{role.Name}'
                        WHERE dbo.Roles.Id = {role.Id}
                      END");
            }
            _context.Database.ExecuteSqlCommand(sqlCommand.ToString());
        }
    }
}
