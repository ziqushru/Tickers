using System.Net;
using Entities.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace AspDotNet.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void configureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(app_error =>
        {
            app_error.Run(async context =>
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var context_feature = context.Features.Get<IExceptionHandlerFeature>();
                if (context_feature == null) return;

                var logger = (ILogger) app.ApplicationServices.GetService(typeof(ILogger))!;

                // ReSharper disable once ExceptionPassedAsTemplateArgumentProblem
                if (true) logger.Error("Something went wrong: {Error}", context_feature.Error);

                await context.Response.WriteAsync(new ErrorResponseDto()
                {
                    status_code = context.Response.StatusCode,
                    message = "Internal Server Error."
                }.ToString());
            });
        });
    }
}
