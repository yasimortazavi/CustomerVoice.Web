using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CustomerVoice.Application.Interfaces.Persistence;
using CustomerVoice.Infrastructure.Persistence.Configurations;




namespace CustomerVoice.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<DissatisfactionReason> DissatisfactionReasons { get; set; }

        public DbSet<ComplaintDissatisfactionReason>
            ComplaintDissatisfactionReasons
        { get; set; }

        public DbSet<ComplaintAttachment> ComplaintAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}