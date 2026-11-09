using CustomerVoice.Application.DTOs.Complaints;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerVoice.Web.Models.Complaints
{
    public class ComplaintSearchViewModel
    {
        public string? Search { get; set; }

        public int? ProjectId { get; set; }

        public int? ServiceTypeId { get; set; }

        public string? Status { get; set; }


        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public ComplaintPagedResultDto Result { get; set; }
            = new ComplaintPagedResultDto();

        public List<SelectListItem> Projects { get; set; }
            = new List<SelectListItem>();

        public List<SelectListItem> ServiceTypes { get; set; }
            = new List<SelectListItem>();
    }
}