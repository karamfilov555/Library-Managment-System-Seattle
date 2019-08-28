using LMS.Models;
using LMS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //one-to-one book-isbn
            //builder
            //    .HasOne(i => i.Isbn)
            //    .WithOne(b => b.Book)
            //    .HasForeignKey<Isbn>(bi => bi.BookId);

        }
    }
}
