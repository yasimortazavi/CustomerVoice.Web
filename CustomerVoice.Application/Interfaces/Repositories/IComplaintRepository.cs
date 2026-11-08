using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Domain.Entities;

namespace CustomerVoice.Application.Interfaces.Repositories
{
    public interface IComplaintRepository
    {
        Task AddAsync(
            Complaint complaint,
            CancellationToken cancellationToken = default);
    }
}
