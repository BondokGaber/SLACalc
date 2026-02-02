using Microsoft.Extensions.Configuration;
using SLACalc.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _uploadPath;

        public LocalFileStorageService(IConfiguration configuration)
        {
            _uploadPath = configuration["FileStorage:UploadPath"] ?? "Uploads";

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<string> SaveFileAsync(string fileName, Stream content, CancellationToken cancellationToken)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";
            var filePath = Path.Combine(_uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await content.CopyToAsync(fileStream, cancellationToken);
            }

            return filePath;
        }

        public Task<Stream> GetFileAsync(string filePath, CancellationToken cancellationToken)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return Task.FromResult<Stream>(stream);
        }
    }
}
