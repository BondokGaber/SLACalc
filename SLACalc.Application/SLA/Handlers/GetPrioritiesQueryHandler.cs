using MediatR;
using SLACalc.Application.SLA.Queries;
using SLACalc.Domain.Interfaces;


namespace SLACalc.Application.SLA.Handlers
{
    public class GetPrioritiesQueryHandler : IRequestHandler<GetPrioritiesQuery, List<string>>
    {
        private readonly ISlaConfigurationRepository _slaConfigurationRepository;

        public GetPrioritiesQueryHandler(ISlaConfigurationRepository slaConfigurationRepository)
        {
            _slaConfigurationRepository = slaConfigurationRepository;
        }

        public async Task<List<string>> Handle(GetPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var priorities = await _slaConfigurationRepository.GetAllPrioritiesAsync(cancellationToken);
            return priorities.ToList();
        }
    }
}
