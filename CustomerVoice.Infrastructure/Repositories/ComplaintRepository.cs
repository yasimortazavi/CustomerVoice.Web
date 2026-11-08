using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Domain.Entities;
using CustomerVoice.Infrastructure.Persistence;

namespace CustomerVoice.Infrastructure.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            Complaint complaint,
            CancellationToken cancellationToken = default)
        {
            await _context.Complaints.AddAsync(
                complaint, cancellationToken);
        }
    }
}