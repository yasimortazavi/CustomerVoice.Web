using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Domain.Entities;

namespace CustomerVoice.Application.Interfaces.Repositories
{
    public interface IComplaintRepository
    {
        Task AddAsync(
            Complaint complaint,
            CancellationToken cancellationToken = default);
        Task<List<Complaint>> GetAllAsync(
    CancellationToken cancellationToken = default);

        Task<(List<Complaint> Items, int TotalCount)> GetPagedAsync(
            string? search,
            int? projectId,
            int? serviceTypeId,
            string? status,
            DateTime? fromDate,
            DateTime? toDate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);


        Task<ComplaintAttachment?> GetAttachmentByComplaintIdAsync(
    int complaintId,
    CancellationToken cancellationToken = default);

        Task<Complaint?> GetByIdAsync(
    int complaintId,
    CancellationToken cancellationToken = default);

        Task DeleteAsync(
            Complaint complaint,
            CancellationToken cancellationToken = default);

    }
}
