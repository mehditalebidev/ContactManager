using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using ErrorOr;
using MediatR;

namespace ContactManager.WebApi.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Log the request
            var requestName = typeof(TRequest).Name;
            LogInformation($"Handling {requestName}", request);

            TResponse response;

            try
            {
                response = await next();
            }
            catch (Exception ex)
            {
                // Log the exception if there is one
                LogInformation($"Exception thrown in handling {requestName}", ex);
                throw;
            }

            // Log the response
            LogInformation($"Handled {requestName}", response);

            return response;
        }

        private void LogInformation(string message, object data)
        {
            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logDirectory); // Ensure directory exists
            var logFilePath = Path.Combine(logDirectory, $"{DateTime.UtcNow:yyyy-MM-dd}.log");
        
            var logEntry = new
            {
                TimeStamp = DateTime.UtcNow,
                Message = message,
                Data = data
            };

            var logText = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true });
        
            // Append to the log file for the day
            File.AppendAllText(logFilePath, logText + Environment.NewLine);
        }
    }