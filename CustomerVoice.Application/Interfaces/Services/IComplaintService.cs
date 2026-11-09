using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.DTOs.Complaints;


namespace CustomerVoice.Application.Interfaces.Services
{
    public interface IComplaintService
    {
        Task<ComplaintResultDto> CreateAsync(
            CreateComplaintDto dto,
            CancellationToken cancellationToken = default);

        Task<List<ComplaintListItemDto>> GetAllAsync(
              CancellationToken cancellationToken = default);

        Task<ComplaintAttachmentDownloadDto?> GetAttachmentForDownloadAsync(
            int complaintId,
             CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(
            int complaintId,
            CancellationToken cancellationToken = default);


        Task<ComplaintPagedResultDto> GetPagedAsync(
            string? search,
            int? projectId,
            int? serviceTypeId,
            string? status,
            DateTime? fromDate,
            DateTime? toDate,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

    }
}
