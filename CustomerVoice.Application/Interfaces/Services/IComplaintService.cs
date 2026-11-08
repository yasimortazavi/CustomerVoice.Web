using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.DTOs.Complaints;

namespace CustomerVoice.Application.Interfaces.Services
{
    public interface IComplaintService
    {
        Task<ComplaintResultDto> CreateAsync(
            CreateComplaintDto dto,
            CancellationToken cancellationToken = default);
    }
}
