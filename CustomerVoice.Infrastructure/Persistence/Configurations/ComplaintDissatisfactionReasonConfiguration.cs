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
    public class ComplaintDissatisfactionReasonConfiguration
        : IEntityTypeConfiguration<ComplaintDissatisfactionReason>
    {
        public void Configure(
            EntityTypeBuilder<ComplaintDissatisfactionReason> builder)
        {
            builder.ToTable("ComplaintDissatisfactionReasons");

            builder.HasKey(x => new
            {
                x.ComplaintId,
                x.DissatisfactionReasonId
            });

            builder.HasOne(x => x.Complaint)
                .WithMany(x => x.DissatisfactionReasons)
                .HasForeignKey(x => x.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.DissatisfactionReason)
                .WithMany(x => x.ComplaintDissatisfactionReasons)
                .HasForeignKey(x => x.DissatisfactionReasonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
