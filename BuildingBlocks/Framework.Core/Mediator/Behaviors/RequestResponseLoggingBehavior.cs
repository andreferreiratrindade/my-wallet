using Framework.Core.LogHelpers;
using Framework.Core.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.Core.Mediator
{

    public class RequestResponseLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TResponse : OutputCommand
            where TRequest : Command<TResponse>

    {
        private readonly ILogger _logger;
        public RequestResponseLoggingBehavior(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.CreateLog(new GenericLog(request.CorrelationId,
                                                    request.MessageType,
                                                    [LogConstants.SERVICE, LogConstants.REQUEST],
                                                    request));
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var response = await next();

            watch.Stop();
            _logger.CreateLog(new ResponseLog(request.CorrelationId,
                                                      request.MessageType,
                                                      [LogConstants.SERVICE, LogConstants.RESPONSE],
                                                      response,
                                                      watch.ElapsedMilliseconds));

            return response;
        }
    }
}
