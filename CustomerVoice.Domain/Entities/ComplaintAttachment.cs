using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Domain.Entities
{
    public class ComplaintAttachment
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }

        public Complaint Complaint { get; set; } = null!;

        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
