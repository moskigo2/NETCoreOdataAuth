using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;


namespace CoreTestApp.Services
{
    public static class ErrorHandler
    {

        public static IApplicationBuilder UseErrorHandling (this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a =>
            {
                a.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;
                    //exception = exception is AggregateException ag ? ag.InnerException : exception;
                    var details = new ProblemDetails();
                    details.Status = (int)HttpStatusCode.BadRequest;

                    switch (exception)
                    {
                        case DbUpdateConcurrencyException se:
                            {
                                details.Title = "Problem with row version";
                                details.Detail = "Record you are trying to update has been changed already";
                                details.Status = (int)HttpStatusCode.Conflict;
                                break;
                            }
                        case DbUpdateException updEx:
                            {
                                details.Title = "Update exception";
                                break;
                            }
                        default:
                            {
                                details.Title = "Exception";
                                break;
                            }
                    }

                    context.Response.StatusCode = details.Status.Value;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(details));

                });
            });

            return app;

        }

    }
}
