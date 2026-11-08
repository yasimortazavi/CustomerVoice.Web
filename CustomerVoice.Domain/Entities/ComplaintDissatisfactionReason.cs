using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Domain.Entities
{
    public class ComplaintDissatisfactionReason
    {
        public int ComplaintId { get; set; }
        public Complaint Complaint { get; set; } = null!;

        public int DissatisfactionReasonId { get; set; }
        public DissatisfactionReason DissatisfactionReason { get; set; } = null!;
    }
}
