using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.ApplicationCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicatinCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
