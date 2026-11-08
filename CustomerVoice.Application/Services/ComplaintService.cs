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
    }
}
