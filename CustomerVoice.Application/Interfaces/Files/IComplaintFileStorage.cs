using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerVoice.Application.Interfaces.Files
{
    public interface IComplaintFileStorage
    {
        Task SaveAsync(
            Stream fileStream,
            string storedFileName,
            CancellationToken cancellationToken = default);

        Task<Stream?> OpenReadAsync(
            string storedFileName,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string storedFileName,
            CancellationToken cancellationToken = default);
    }
}
