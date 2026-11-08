using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.Interfaces.Files;

namespace CustomerVoice.Infrastructure.Files
{
    public class LocalComplaintFileStorage : IComplaintFileStorage
    {
        private readonly string _storagePath;

        public LocalComplaintFileStorage()
        {
            _storagePath = Path.Combine(
                AppContext.BaseDirectory,
                "App_Data",
                "ComplaintAttachments");

            Directory.CreateDirectory(_storagePath);
        }

        public async Task SaveAsync(
            Stream fileStream,
            string storedFileName,
            CancellationToken cancellationToken = default)
        {
            var filePath = Path.Combine(
                _storagePath,
                storedFileName);

            await using var outputStream = new FileStream(
                filePath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);

            await fileStream.CopyToAsync(
                outputStream,
                cancellationToken);
        }
        public Task<Stream?> OpenReadAsync(
    string storedFileName,
    CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var safeFileName = Path.GetFileName(storedFileName);

            var filePath = Path.Combine(
                _storagePath,
                safeFileName);

            if (!File.Exists(filePath))
            {
                return Task.FromResult<Stream?>(null);
            }

            Stream stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);

            return Task.FromResult<Stream?>(stream);
        }

        public Task DeleteAsync(
    string storedFileName,
    CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var safeFileName = Path.GetFileName(storedFileName);

            var filePath = Path.Combine(
                _storagePath,
                safeFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}