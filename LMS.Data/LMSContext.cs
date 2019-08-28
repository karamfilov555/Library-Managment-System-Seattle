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
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReserveBookConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryRegistryConfiguration());
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
