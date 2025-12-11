using App.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DependencyInjection
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnectionString"),
                new MySqlServerVersion(new Version(8, 0, 29))));

            Console.WriteLine(configuration.GetConnectionString("DefaultConnectionString"));

            return services;
        }
    }
}
