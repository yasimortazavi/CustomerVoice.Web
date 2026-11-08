using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Application.DTOs.Complaints
{
    public class ComplaintResultDto
    {
        public int Id { get; set; }

        public string TrackingCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
