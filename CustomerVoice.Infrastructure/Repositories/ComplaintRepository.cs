using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Domain.Entities;
using CustomerVoice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Complaint>> GetAllAsync(
    CancellationToken cancellationToken = default)
        {
            return await _context.Complaints
                .AsNoTracking()
                .Include(x => x.Project)
                .Include(x => x.ServiceType)
                .Include(x => x.Attachment)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<ComplaintAttachment?> GetAttachmentByComplaintIdAsync(
    int complaintId,
    CancellationToken cancellationToken = default)
        {
            return await _context.ComplaintAttachments
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.ComplaintId == complaintId,
                    cancellationToken);
        }

        public async Task<Complaint?> GetByIdAsync(
    int complaintId,
    CancellationToken cancellationToken = default)
        {
            return await _context.Complaints
                .Include(x => x.Attachment)
                .FirstOrDefaultAsync(
                    x => x.Id == complaintId,
                    cancellationToken);
        }

        public Task DeleteAsync(
            Complaint complaint,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _context.Complaints.Remove(complaint);

            return Task.CompletedTask;
        }
    }
}