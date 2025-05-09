﻿using System.Net;
using System.Text.Json;
using TalabatPL.Erros;

namespace TalabatPL.Middlewares
{
    public class ExceptionResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionResponseMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionResponseMiddleware(RequestDelegate next, ILogger<ExceptionResponseMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment() ? new ApiResponseException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiResponseException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(json);



            }


        }


    }
}
