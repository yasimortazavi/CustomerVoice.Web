using CustomerVoice.Application.DTOs.Complaints;
using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Application.Interfaces.Services;
using CustomerVoice.Web.ViewModels.Complaints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerVoice.Web.Models.Complaints;



namespace CustomerVoice.Web.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly IComplaintService _complaintService;
        private readonly IComplaintReferenceDataRepository _referenceDataRepository;
        private readonly IValidator<CreateComplaintDto> _validator;

        public ComplaintsController(
            IComplaintService complaintService,
            IComplaintReferenceDataRepository referenceDataRepository,
            IValidator<CreateComplaintDto> validator)
        {
            _complaintService = complaintService;
            _referenceDataRepository = referenceDataRepository;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Create(
            CancellationToken cancellationToken)
        {
            var model = new ComplaintCreateViewModel();

            await LoadReferenceDataAsync(model, cancellationToken);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            ComplaintCreateViewModel model,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(
                (CreateComplaintDto)model,
                cancellationToken);

            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }

            if (model.Attachment != null &&
                model.Attachment.Length > 0)
            {
                var allowedExtensions = new[]
                 {
                                ".pdf",
                                ".jpg",
                                ".jpeg",
                                ".png"
                            };

                var extension = Path.GetExtension(
                    model.Attachment.FileName);

                if (!allowedExtensions.Contains(
                    extension,
                    StringComparer.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(
                        nameof(model.Attachment),
                        "فرمت فایل انتخاب ‌شده مجاز نیست.");
                }

                const long maxFileSize = 5 * 1024 * 1024;

                if (model.Attachment.Length > maxFileSize)
                {
                    ModelState.AddModelError(
                        nameof(model.Attachment),
                        "حجم فایل نباید بیشتر از ۵ مگابایت باشد.");
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadReferenceDataAsync(model, cancellationToken);
                return View(model);
            }

            using var fileStream = model.Attachment?.OpenReadStream();


            if (model.Attachment != null && fileStream != null)
            {
                var extension = Path.GetExtension(
                    model.Attachment.FileName);

                var contentType = extension.ToLowerInvariant() switch
                {
                    ".pdf" => "application/pdf",
                    ".jpg" => "image/jpeg",
                    ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    _ => "application/octet-stream"
                };

                model.AttachmentData = new ComplaintAttachmentDto
                {
                    FileStream = fileStream,
                    OriginalFileName = Path.GetFileName(
                        model.Attachment.FileName),
                    ContentType = contentType,
                    FileSize = model.Attachment.Length
                };
            }
            try
            {
                var result = await _complaintService.CreateAsync(
                    model,
                    cancellationToken);

                return RedirectToAction(
                    nameof(Success),
                    new { trackingCode = result.TrackingCode });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                await LoadReferenceDataAsync(model, cancellationToken);

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Success(string trackingCode)
        {
            ViewBag.TrackingCode = trackingCode;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(
           ComplaintSearchViewModel model,
           CancellationToken cancellationToken)
        {
            model.PageNumber = Math.Max(1, model.PageNumber);
            model.PageSize = Math.Clamp(model.PageSize, 1, 100);

            var result = await _complaintService.GetPagedAsync(
                model.Search,
                model.ProjectId,
                model.ServiceTypeId,
                model.Status,
                model.FromDate,
                model.ToDate,
                model.PageNumber,
                model.PageSize,
                cancellationToken);

            model.Result = result;

            var projects =
                await _referenceDataRepository.GetActiveProjectsAsync(
                    cancellationToken);

            var serviceTypes =
                await _referenceDataRepository.GetActiveServiceTypesAsync(
                    cancellationToken);

            model.Projects = projects.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToList();

            model.ServiceTypes = serviceTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadAttachment(
    int complaintId,
    CancellationToken cancellationToken)
        {
            var attachment =
                await _complaintService.GetAttachmentForDownloadAsync(
                    complaintId,
                    cancellationToken);

            if (attachment == null)
                return NotFound();

            return File(
                attachment.FileStream,
                attachment.ContentType,
                Path.GetFileName(attachment.OriginalFileName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(
            int complaintId,
            CancellationToken cancellationToken)
        {
            var deleted = await _complaintService.DeleteAsync(
                complaintId,
                cancellationToken);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        private async Task LoadReferenceDataAsync(
            ComplaintCreateViewModel model,
            CancellationToken cancellationToken)
        {
            var projects =
                await _referenceDataRepository.GetActiveProjectsAsync(
                    cancellationToken);

            var serviceTypes =
                await _referenceDataRepository.GetActiveServiceTypesAsync(
                    cancellationToken);

            var reasons =
                await _referenceDataRepository.GetAllActiveDissatisfactionReasonsAsync(
                    cancellationToken);

            model.Projects = projects.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });

            model.ServiceTypes = serviceTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });

            model.DissatisfactionReasons = reasons.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title,
                Selected = model.DissatisfactionReasonIds.Contains(x.Id)
            });
        }

    }
}