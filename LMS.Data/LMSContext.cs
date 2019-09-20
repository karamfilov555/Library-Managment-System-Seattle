using LMS.Models;
using LMS.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data
{
    public class LMSContext : IdentityDbContext<User,Role,string>
    {
        public LMSContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookSubject> Subjects { get; set; }
        public DbSet<HistoryRegistry> HistoryRegistries { get; set; }
        public DbSet<Isbn> Isbns { get; set; }
        public DbSet<ReserveBook> ReservedBooks { get; set; }
        public DbSet<SubjectCategory> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<BanRecord> BanRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HistoryRegistry>().HasKey(hr => new { hr.BookId, hr.UserId });
            builder.Entity<ReserveBook>().HasKey(rb => new { rb.BookId, rb.UserId });
            builder.Entity<BookSubject>().HasKey(bs => new { bs.BookId, bs.SubjectCategoryId });
            builder.Entity<Role>().HasKey(r => r.Id);
            builder.Entity<Isbn>().HasOne(b => b.Book).WithOne(i => i.Isbn).HasForeignKey<Book>(ii => ii.IsbnId);
            builder.Entity<BookRating>().HasOne(b => b.Book).WithOne(i => i.BookRating).HasForeignKey<Book>(ii => ii.BookRatingId);
            base.OnModelCreating(builder);
        }

    }
}
