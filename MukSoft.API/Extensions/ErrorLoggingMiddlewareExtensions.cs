using Microsoft.AspNetCore.Builder;
using MukSoft.API.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace MukSoft.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ErrorLoggingMiddlewareExtensions
    {
        public static void UseErrorLogging(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(err => err.Run(ErrorLogging.HandleAsync));
        }
    }
}
