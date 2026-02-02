using MediatR;
using SLACalc.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Application.SLA.Commands
{
    public class CalculateSlaCommand : IRequest<SlaCalculationResult>
    {
        public string Priority { get; set; } = string.Empty;
        public DateTime CaptureDateTime { get; set; }
        public List<FileUpload>? Files { get; set; }
    }

    public class FileUpload
    {
        public string FileName { get; set; } = string.Empty;
        public Stream Content { get; set; } = null!;
    }
}
