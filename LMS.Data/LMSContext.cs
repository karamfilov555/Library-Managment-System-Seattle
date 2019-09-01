using System;
using LMS.Data.Configurations;
using LMS.Models;
using LMS.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSContext : DbContext
    {
        public LMSContext()
        {
        }
        public LMSContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<HistoryRegistry> HistoryRegistries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Isbn> Isbns { get; set; }
        public DbSet<RecordFines> RecordFines { get; set; }
        public DbSet<SubjectCategory> SubjectCategories { get; set; }
        public DbSet<BookSubject> BooksSubjects { get; set; }
        public DbSet<ReserveBook> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configurator.ProvideConnectionString());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReserveBookConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryRegistryConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new IsbnConfiguration());
            modelBuilder.ApplyConfiguration(new FineConfiguration());
           
            base.OnModelCreating(modelBuilder);
        }

        public object First()
        {
            throw new NotImplementedException();
        }
    }
}
