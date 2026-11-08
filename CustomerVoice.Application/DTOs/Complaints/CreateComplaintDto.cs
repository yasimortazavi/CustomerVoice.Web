using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Application.DTOs.Complaints
{
    public class CreateComplaintDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string? LandlineNumber { get; set; }

        public string? Email { get; set; }

        public int ProjectId { get; set; }

        public int ServiceTypeId { get; set; }

        public List<int> DissatisfactionReasonIds { get; set; }
            = new List<int>();

        public string Description { get; set; } = string.Empty;

        public string? SuggestedSolution { get; set; }
        public ComplaintAttachmentDto? AttachmentData { get; set; }
    }
}