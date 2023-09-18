﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TestCoreDockerService.Helper;

namespace TestCoreDocker.MiddleWares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(next);
                
                await next(context).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Some Error Happened while Fetching weather"); //Log the exception here
                if (context!=null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    ProblemDetails problem = new ProblemDetails { 
                        Status= (int)HttpStatusCode.InternalServerError,
                        Title="Internal Server Error",
                        Detail="Some Internal Server Error happened while fetching weather data"
                    };
                    string json= JsonConvert.SerializeObject(problem);
                    context.Response?.WriteAsync(json); // This will return a response with Error message
                }
            }
        }
    }
}
