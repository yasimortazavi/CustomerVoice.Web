using CustomerVoice.Application.DTOs.Complaints;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CustomerVoice.Web.ViewModels.Complaints
{
    public class ComplaintCreateViewModel : CreateComplaintDto
    {
        public IEnumerable<SelectListItem> Projects { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ServiceTypes { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> DissatisfactionReasons { get; set; }
            = new List<SelectListItem>();
    }
}