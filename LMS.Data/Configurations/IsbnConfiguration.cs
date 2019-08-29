using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LMS.Models.Models;
using LMS.Models;

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
