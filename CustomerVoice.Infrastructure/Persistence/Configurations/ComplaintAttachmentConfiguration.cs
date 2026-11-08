using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomerVoice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerVoice.Infrastructure.Persistence.Configurations
{
    public class ComplaintAttachmentConfiguration
        : IEntityTypeConfiguration<ComplaintAttachment>
    {
        public void Configure(EntityTypeBuilder<ComplaintAttachment> builder)
        {
            builder.ToTable("ComplaintAttachments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OriginalFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.StoredFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FileSize)
                .IsRequired();

            builder.Property(x => x.UploadedAt)
                .IsRequired();

            // هر شکایت حداکثر یک فایل پیوست دارد
            builder.HasIndex(x => x.ComplaintId)
                .IsUnique();

            builder.HasOne(x => x.Complaint)
                .WithOne(x => x.Attachment)
                .HasForeignKey<ComplaintAttachment>(x => x.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
