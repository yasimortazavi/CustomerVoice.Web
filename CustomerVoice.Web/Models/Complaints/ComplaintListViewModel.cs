using CustomerVoice.Application.DTOs.Complaints;

namespace CustomerVoice.Web.Models.Complaints
{
    public class ComplaintListViewModel
    {
        public List<ComplaintListItemDto> Complaints { get; set; }
            = new List<ComplaintListItemDto>();
    }
}