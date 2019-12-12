//using Microsoft.AspNetCore.Http;
using CoreTestApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;


namespace CoreTestApp.Services
{
    public static class AuthorizeCustom
    {
        public static IServiceCollection AddAuthorizeCustom(this IServiceCollection services)
        {
            services.AddAuthorization(a =>
            {
                a.AddPolicy(PolicyNameCustom.Admin, builder => builder.RequireClaim(ClaimTypes.Role, IncludeRoles.Admin));
                a.AddPolicy(PolicyNameCustom.User, builder => builder.RequireClaim(ClaimTypes.Role, IncludeRoles.User));
            });

            return services;
        }
    }
}
