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
    public class ComplaintConfiguration
        : IEntityTypeConfiguration<Complaint>
    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {
            builder.ToTable("Complaints");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TrackingCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.MobileNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.LandlineNumber)
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(x => x.SuggestedSolution)
                .HasMaxLength(4000);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.TrackingCode)
                .IsUnique();

            builder.HasOne(x => x.Project)
         .WithMany(x => x.Complaints)
         .HasForeignKey(x => x.ProjectId)
         .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ServiceType)
             .WithMany(x => x.Complaints)
             .HasForeignKey(x => x.ServiceTypeId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
