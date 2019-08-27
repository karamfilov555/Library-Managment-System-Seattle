using LMS.Data.Configurations;
using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSContext : DbContext
    {
        public LMSContext()
        {

        }
        public LMSContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<HistoryRegistry> HistoryRegistries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SubjectCategory> SubjectCategories { get; set; }
        public DbSet<BookSubject> BooksSubjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString =
                @"Server=.\SQLEXPRESS;Database=LMS_ToNik;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());


            modelBuilder
                .Entity<Book>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            base.OnModelCreating(modelBuilder);
        }
    }
}
