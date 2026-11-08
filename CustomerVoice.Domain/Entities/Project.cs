using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Complaint> Complaints { get; set; }
                    = new List<Complaint>();
    }
}