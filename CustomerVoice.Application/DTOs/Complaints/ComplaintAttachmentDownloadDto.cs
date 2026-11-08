using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Application.DTOs.Complaints
{
    public class ComplaintAttachmentDownloadDto
    {
        public Stream FileStream { get; set; } = Stream.Null;

        public string OriginalFileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;
    }
}
