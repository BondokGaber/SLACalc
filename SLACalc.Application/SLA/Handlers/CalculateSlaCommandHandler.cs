using MediatR;
using SLACalc.Application.SLA.Commands;
using SLACalc.Domain.Interfaces;
using SLACalc.Domain.ValueObjects;
using SLACalc.Application.Common.Interfaces;
namespace SLACalc.Application.SLA.Handlers
{
    public class CalculateSlaCommandHandler : IRequestHandler<CalculateSlaCommand, SlaCalculationResult>
    {
        private readonly IBusinessHourCalculator _businessHourCalculator;
        private readonly ISlaConfigurationRepository _slaConfigurationRepository;
        private readonly IFileStorageService _fileStorageService;

        public CalculateSlaCommandHandler(
            IBusinessHourCalculator businessHourCalculator,
            ISlaConfigurationRepository slaConfigurationRepository,
            IFileStorageService fileStorageService)
        {
            _businessHourCalculator = businessHourCalculator;
            _slaConfigurationRepository = slaConfigurationRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<SlaCalculationResult> Handle(CalculateSlaCommand request, CancellationToken cancellationToken)
        {
            // Validate SLA configuration exists
            var slaConfig = await _slaConfigurationRepository.GetByPriorityAsync(request.Priority, cancellationToken);
            if (slaConfig == null || !slaConfig.IsActive)
            {
                throw new ArgumentException($"No active SLA configuration found for priority: {request.Priority}");
            }

            // Handle file uploads if any
            if (request.Files != null && request.Files.Any())
            {
                foreach (var file in request.Files)
                {
                    await _fileStorageService.SaveFileAsync(file.FileName, file.Content, cancellationToken);
                }
            }

            // Calculate deadline
            var deadline = _businessHourCalculator.CalculateDeadline(request.CaptureDateTime, slaConfig.SlaHours);

            return new SlaCalculationResult(
                request.CaptureDateTime,
                request.Priority,
                slaConfig.SlaHours,
                deadline);
        }
    }
}
