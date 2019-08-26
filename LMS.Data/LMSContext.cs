using LMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS.Data
{
    public class LMSContext : DbContext
    {
        public LMSContext()
        {
                
        }
        public LMSContext(DbContextOptions options)
        {

        }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<HistoryRegistry> HistoryRegistry { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SubjectCategory> SubjectCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString =
                @"Server=.\SQLEXPRESS;Database=LMS_ToNik;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
