using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LMS.Models;

namespace LMS.Data.Configurations
{
    public class FineConfiguration : IEntityTypeConfiguration<RecordFines>
    {
        public void Configure(EntityTypeBuilder<RecordFines> builder)
        {
            //one-to-one RecordFines-user
            builder
                .HasOne(f => f.User)
                .WithOne(u => u.RecordFines)
                .HasForeignKey<User>(u => u.RecordFinesId);
        }
    }
}
