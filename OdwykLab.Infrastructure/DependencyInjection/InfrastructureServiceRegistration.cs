using Microsoft.Extensions.DependencyInjection;
using App.Domain.Abstractions;
using App.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using App.Application.Interfaces.Authentication;



namespace App.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenGeneratorRepository, JwtTokenGenerator>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IDayRepository, DayRepository>();

            JwtServiceRegistration.AddJwtServices(services, configuration);
            SwaggerServiceRegistration.AddSwaggerServices(services);
            CorsServiceRegistration.AddCorsServices(services);
            DatabaseServiceRegistration.AddDatabaseServices(services, configuration);
            return services;
        }
    }
}
