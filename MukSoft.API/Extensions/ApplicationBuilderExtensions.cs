using Microsoft.AspNetCore.Builder;
using MukSoft.Core.Security;
using System.Diagnostics.CodeAnalysis;

namespace MukSoft.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAntiforgeryTokens(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
        }
    }
}
