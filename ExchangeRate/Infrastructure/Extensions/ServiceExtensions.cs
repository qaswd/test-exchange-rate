using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;


namespace Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<ICurrencyRateProvider, CbrCurrencyRateProvider>();
            return services;
        }
    }
}
