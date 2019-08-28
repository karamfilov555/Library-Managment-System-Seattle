using LMS.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Configurations
{
    public class ReserveBookConfiguration : IEntityTypeConfiguration<ReserveBook>
    {
        public void Configure(EntityTypeBuilder<ReserveBook> builder)
        {
            //many-to-many
            builder
                .HasKey(key => new { key.BookId, key.UserId });

            builder
                .HasOne(b => b.Book)
                .WithMany(rb => rb.ReservedBooks)
                .HasForeignKey(b => b.BookId);

            builder
                .HasOne(u => u.User)
                .WithMany(rb => rb.ReservedBooks)
                .HasForeignKey(u => u.UserId);
        }
    }
}
