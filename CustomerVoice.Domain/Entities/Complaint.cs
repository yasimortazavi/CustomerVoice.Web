using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Domain.Entities
{
    public class Complaint
    {
        public int Id { get; set; }

        public string TrackingCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string? LandlineNumber { get; set; }

        public string? Email { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public string? SuggestedSolution { get; set; }


        public string Status { get; set; } = "Submitted";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ComplaintDissatisfactionReason>
            DissatisfactionReasons
        { get; set; }
            = new List<ComplaintDissatisfactionReason>();

        public ComplaintAttachment? Attachment { get; set; }
    }
}