using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Application.DTOs.Complaints
{
    public class ComplaintListItemDto
    {
        public int Id { get; set; }
        public string TrackingCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProjectTitle { get; set; } = string.Empty;
        public string ServiceTypeTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? SuggestedSolution { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool HasAttachment { get; set; }
    }
}
