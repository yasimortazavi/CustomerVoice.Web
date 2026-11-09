using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.DTOs.Complaints;
using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Application.Interfaces.Persistence;
using CustomerVoice.Application.Interfaces.Services;
using CustomerVoice.Domain.Entities;
using CustomerVoice.Application.Interfaces.Files;
using System.IO;

namespace CustomerVoice.Application.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _repository;
        private readonly IComplaintReferenceDataRepository _referenceDataRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComplaintFileStorage _fileStorage;

        public ComplaintService(
            IComplaintRepository repository,
            IComplaintReferenceDataRepository referenceDataRepository,
            IUnitOfWork unitOfWork,
            IComplaintFileStorage fileStorage)
        {
            _repository = repository;
            _referenceDataRepository = referenceDataRepository;
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task<ComplaintResultDto> CreateAsync(
            CreateComplaintDto dto,
            CancellationToken cancellationToken = default)
        {
            var isProjectActive =
                await _referenceDataRepository.IsProjectActiveAsync(
                    dto.ProjectId, cancellationToken);

            if (!isProjectActive)
                throw new InvalidOperationException(
                    "پروژه انتخاب‌شده معتبر یا فعال نیست.");

            var isServiceTypeActive =
                await _referenceDataRepository.IsServiceTypeActiveAsync(
                    dto.ServiceTypeId, cancellationToken);

            if (!isServiceTypeActive)
                throw new InvalidOperationException(
                    "نوع خدمات انتخاب‌شده معتبر یا فعال نیست.");

            var selectedIds = dto.DissatisfactionReasonIds
                .Distinct()
                .ToList();

            var activeReasons =
                await _referenceDataRepository.GetActiveDissatisfactionReasonsAsync(
                    selectedIds, cancellationToken);

            if (activeReasons.Count != selectedIds.Count)
                throw new InvalidOperationException(
                    "یک یا چند علت نارضایتی انتخاب‌شده معتبر یا فعال نیست.");
            string? storedFileName = null;

            if (dto.AttachmentData != null)
            {
                var extension = Path.GetExtension(
                    dto.AttachmentData.OriginalFileName);

                storedFileName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
            }
            var complaint = new Complaint
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobileNumber = dto.MobileNumber,
                LandlineNumber = dto.LandlineNumber,
                Email = dto.Email,
                ProjectId = dto.ProjectId,
                ServiceTypeId = dto.ServiceTypeId,
                Description = dto.Description,
                SuggestedSolution = dto.SuggestedSolution,
                Attachment = dto.AttachmentData != null
                            ? new ComplaintAttachment
                            {
                                OriginalFileName = Path.GetFileName(
                                    dto.AttachmentData.OriginalFileName),
                                StoredFileName = storedFileName!,
                                ContentType = dto.AttachmentData.ContentType,
                                FileSize = dto.AttachmentData.FileSize,
                                UploadedAt = DateTime.UtcNow
                            }
                            : null,
                TrackingCode = Guid.NewGuid()
                    .ToString("N")[..10].ToUpperInvariant(),
                Status = "Submitted",
                CreatedAt = DateTime.UtcNow,

                DissatisfactionReasons = activeReasons
                    .Select(reason => new ComplaintDissatisfactionReason
                    {
                        DissatisfactionReasonId = reason.Id
                    })
                    .ToList()
            };
            bool fileSaved = false;

            try
            {
                if (dto.AttachmentData != null)
                {
                    await _fileStorage.SaveAsync(
                        dto.AttachmentData.FileStream,
                        storedFileName!,
                        cancellationToken);

                    fileSaved = true;
                }

                await _repository.AddAsync(
                    complaint, cancellationToken);

                await _unitOfWork.SaveChangesAsync(
                    cancellationToken);
            }
            catch
            {
                if (fileSaved)
                {
                    await _fileStorage.DeleteAsync(
                        storedFileName!,
                        CancellationToken.None);
                }

                throw;
            }
            return new ComplaintResultDto
            {
                Id = complaint.Id,
                TrackingCode = complaint.TrackingCode,
                CreatedAt = complaint.CreatedAt
            };
        }


        public async Task<List<ComplaintListItemDto>> GetAllAsync(
    CancellationToken cancellationToken = default)
        {
            var complaints = await _repository.GetAllAsync(
                cancellationToken);

            return complaints.Select(x => new ComplaintListItemDto
            {
                Id = x.Id,
                TrackingCode = x.TrackingCode,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ProjectTitle = x.Project.Title,
                ServiceTypeTitle = x.ServiceType.Title,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                Description = x.Description,
                SuggestedSolution = x.SuggestedSolution,
                HasAttachment = x.Attachment != null
            }).ToList();
        }

        public async Task<ComplaintPagedResultDto> GetPagedAsync(
                        string? search,
                        int? projectId,
                        int? serviceTypeId,
                        string? status,
                        DateTime? fromDate,
                        DateTime? toDate,
                        int pageNumber,
                        int pageSize,
                        CancellationToken cancellationToken = default)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var result = await _repository.GetPagedAsync(
                search,
                projectId,
                serviceTypeId,
                status,
                fromDate,
                toDate,
                pageNumber,
                pageSize,
                cancellationToken);

            return new ComplaintPagedResultDto
            {
                Items = result.Items.Select(x => new ComplaintListItemDto
                {
                    Id = x.Id,
                    TrackingCode = x.TrackingCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ProjectTitle = x.Project.Title,
                    ServiceTypeTitle = x.ServiceType.Title,
                    Description = x.Description,
                    SuggestedSolution = x.SuggestedSolution,
                    HasAttachment = x.Attachment != null,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                }).ToList(),

                TotalCount = result.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ComplaintAttachmentDownloadDto?>
                  GetAttachmentForDownloadAsync(
                         int complaintId,
                         CancellationToken cancellationToken = default) 
        {
            var attachment =
                await _repository.GetAttachmentByComplaintIdAsync(
                    complaintId,
                    cancellationToken);

            if (attachment == null)
                return null;

            var fileStream = await _fileStorage.OpenReadAsync(
                attachment.StoredFileName,
                cancellationToken);

            if (fileStream == null)
                return null;

            return new ComplaintAttachmentDownloadDto
            {
                FileStream = fileStream,
                OriginalFileName = attachment.OriginalFileName,
                ContentType = attachment.ContentType
            };
        }
        public async Task<bool> DeleteAsync(
    int complaintId,
    CancellationToken cancellationToken = default)
        {
            var complaint = await _repository.GetByIdAsync(
                complaintId,
                cancellationToken);

            if (complaint == null)
                return false;

            var storedFileName = complaint.Attachment?.StoredFileName;

            await _repository.DeleteAsync(
                complaint,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            if (!string.IsNullOrWhiteSpace(storedFileName))
            {
                await _fileStorage.DeleteAsync(
                    storedFileName,
                    CancellationToken.None);
            }

            return true;
        }
    }
}
