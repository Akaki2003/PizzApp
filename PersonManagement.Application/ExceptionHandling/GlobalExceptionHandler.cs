using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PizzApp.Application.GlobalExceptionHandling
{
    public class GlobalExceptionHandler : ProblemDetails
    {
        public const string UnhandlerErrorCode = "UnhandledError";
        private HttpContext _httpContext;
        private Exception _exception;
        private const string errorFile = @"C:\Users\Guram\Desktop\erorrLogging.txt";



        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }

                return null;
            }
            set => Extensions["TraceId"] = value;
        }

        public GlobalExceptionHandler(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;
            File.AppendAllText(errorFile,exception.Message);
            File.AppendAllText(errorFile,exception.Source);
            File.AppendAllText(errorFile,exception.StackTrace);
            File.AppendAllText(errorFile,Instance);


            HandleException((dynamic)exception);
        }


        private void HandleException(Exception exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }
    }
}

