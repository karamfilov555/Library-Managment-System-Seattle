using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LMS.Data;
using LMS.Models;

namespace LMS.Web.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static void SeedDatabaseRole(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LMSContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {

                    var memberRole = "Member";

                    var memberRoleExist = await roleManager.RoleExistsAsync(memberRole);

                    if (!memberRoleExist)
                    {
                        await roleManager.CreateAsync(new Role
                        {
                            Name = memberRole
                        });
                    }

                    var librarianRole = "Librarian";

                    var libRoleExists = await roleManager.RoleExistsAsync(librarianRole);

                    if (!libRoleExists)
                    {
                        await roleManager.CreateAsync(new Role
                        {
                            Name = librarianRole
                        });
                    }

                    var adminRole = "Admin";

                    var exists = await roleManager.RoleExistsAsync(adminRole);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new Role
                        {
                            Name = adminRole
                        });
                    }

                    var adminName = "admin";

                    var adminUser = await userManager.FindByNameAsync(adminName);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            UserName = adminName,
                            Email = "admin@admin.com"
                        };

                        await userManager.CreateAsync(adminUser, "admin123");
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }

                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
