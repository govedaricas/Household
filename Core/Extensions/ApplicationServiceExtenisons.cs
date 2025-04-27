using Application.Features.Household.Queries.GetHouseholdById;
using System.Reflection;
using Application.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetHouseholdByIdQueryHandler).Assembly);

            // Register application services
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
