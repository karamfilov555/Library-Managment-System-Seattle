using LMS.Models;
using LMS.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSContext : IdentityDbContext<User,Role,string>
    {
        public LMSContext()
        {

        }
        public LMSContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SubjectCategory> SubjectCategories { get; set; }
        public DbSet<HistoryRegistry> HistoryRegistries { get; set; }
        public DbSet<ReserveBook> ReservedBooks { get; set; }
        public DbSet<SubjectCategory> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<BanRecord> BanRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HistoryRegistry>().HasKey(hr => new { hr.BookId, hr.UserId });
            builder.Entity<ReserveBook>().HasKey(rb => new { rb.BookId, rb.UserId });
            builder.Entity<Role>().HasKey(r => r.Id);
    
            builder.Entity<BookRating>().HasOne(b => b.Book).WithOne(i => i.BookRating).HasForeignKey<Book>(ii => ii.BookRatingId);
            builder.Entity<BanRecord>().HasOne(u => u.User).WithOne(b => b.BanRecord).HasForeignKey<User>(ii => ii.BanRecordId);
            base.OnModelCreating(builder);
        }

    }
}
