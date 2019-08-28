using LMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Configurations
{
    public class BookSubjectConfiguration : IEntityTypeConfiguration<BookSubject>
    {
        public void Configure(EntityTypeBuilder<BookSubject> builder)
        {
            //many-to-many 
            builder
                .HasKey(key => new { key.BookId, key.SubjectCategoryId });

            builder
                
                .HasOne(b => b.Book)
                .WithMany(bs => bs.BookSubject)
                
                .HasForeignKey(b => b.BookId);

            builder
                .HasOne(s => s.SubjectCategory)
                .WithMany(bs => bs.BookSubject)
                .HasForeignKey(s => s.SubjectCategoryId);
        }
    }
}
