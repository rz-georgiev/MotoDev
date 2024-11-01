using Microsoft.Extensions.Logging;
using MotoDev.Application.Interfaces;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.Persistence;

public class QueryLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public QueryLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, MotoDevDbContext dbContext)
    {
        // Capture the client's IP address
        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

        // Capture the request path and query string
        var requestPath = httpContext.Request.Path;
        var queryString = httpContext.Request.QueryString.ToString();

        // Create a log entry
        var logEntry = new RequestLogEntry
        {
            IpAddress = ipAddress,
            Path = requestPath,
            QueryString = queryString,
            Timestamp = DateTime.UtcNow
        };

        // Save the log entry to the database


        await dbContext.AddAsync(logEntry);
        await dbContext.SaveChangesAsync();
        
        // Log the information (optional)

        // Call the next middleware in the pipeline
        await _next(httpContext);
    }
}