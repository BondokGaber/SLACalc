using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(string fileName, Stream content, CancellationToken cancellationToken = default);
        Task<Stream> GetFileAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
