using LMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Configurations
{
    public class HistoryRegistryConfiguration : IEntityTypeConfiguration<HistoryRegistry>
    {
        public void Configure(EntityTypeBuilder<HistoryRegistry> builder)
        {
            //many-to-many
            builder
                .HasKey(key => new { key.BookId, key.UserId });

            builder
                .HasOne(b => b.Book)
                .WithMany(hr => hr.HistoryRegistries)
                .HasForeignKey(b => b.BookId);

            builder
                .HasOne(u => u.User)
                .WithMany(hr => hr.HistoryRegistries)
                .HasForeignKey(u => u.UserId);
        }
    }
}
