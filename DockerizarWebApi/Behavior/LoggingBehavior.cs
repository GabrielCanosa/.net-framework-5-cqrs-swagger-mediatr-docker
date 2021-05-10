using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerizarWebApi.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public ILogger<LoggingBehavior<TRequest, TResponse>> logger { get; }
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Pre logic

            logger.LogInformation("{Request} is starting.", request.GetType().FullName);
            var timer = Stopwatch.StartNew();
            var response = next();
            timer.Stop();

            // Post logic

            logger.LogInformation("{Request} has finished in {Time}ms.", request.GetType().FullName, timer.ElapsedMilliseconds);
            return response;
        }
    }
}
