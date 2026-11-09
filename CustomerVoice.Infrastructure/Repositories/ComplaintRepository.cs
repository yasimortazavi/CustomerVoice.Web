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
        public async Task<(List<Complaint> Items, int TotalCount)> GetPagedAsync(
                        string? search,
                        int? projectId,
                        int? serviceTypeId,
                        string? status,
                        int pageNumber,
                        int pageSize,
                        CancellationToken cancellationToken = default)
        {
            var query = _context.Complaints
                .AsNoTracking()
                .Include(x => x.Project)
                .Include(x => x.ServiceType)
                .Include(x => x.Attachment)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.TrackingCode.Contains(search) ||
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.MobileNumber.Contains(search));
            }

            if (projectId.HasValue)
                query = query.Where(x => x.ProjectId == projectId.Value);

            if (serviceTypeId.HasValue)
                query = query.Where(x => x.ServiceTypeId == serviceTypeId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.Status == status);

         

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
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