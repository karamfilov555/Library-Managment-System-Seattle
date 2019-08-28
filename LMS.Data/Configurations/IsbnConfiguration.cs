using LMS.Models;
using LMS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Configurations
{
    public class IsbnConfiguration : IEntityTypeConfiguration<Isbn>
    {
        public void Configure(EntityTypeBuilder<Isbn> builder)
        {
            //one-to-one book-isbn
            builder
                .HasOne(b => b.Book)
                .WithOne(i => i.Isbn)
                .HasForeignKey<Book>(ii => ii.IsbnId);

        }
    }
}
