using MediatR;
using Microsoft.Extensions.Logging;

namespace SlaCalculation.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next
           )
        {
            _logger.LogInformation("Handling {RequestName} with {@Request}", typeof(TRequest).Name, request);

            try
            {
                var response = await next();
                _logger.LogInformation("Handled {RequestName} successfully", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling {RequestName}", typeof(TRequest).Name);
                throw;
            }
        }

        
    }
}