using App.Api.Middleware;
using App.Application.Behaviors;
using App.Application.DependencyInjection;
using App.Infrastructure.DependencyInjection;
using MediatR;

namespace App.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddApplicationServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);



            builder.Services.AddAuthorization();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();
            var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
            logger.LogInformation("API started!");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                try
                {
                    var schemeProvider = app.Services.GetRequiredService<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>();
                    var authOptions = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Authentication.AuthenticationOptions>>().Value;
                    var programLogger = app.Services.GetRequiredService<ILogger<Program>>();

                    programLogger.LogInformation("AuthenticationOptions defaults: DefaultScheme={Default}, DefaultAuthenticate={Auth}, DefaultChallenge={Challenge}",
                        authOptions.DefaultScheme, authOptions.DefaultAuthenticateScheme, authOptions.DefaultChallengeScheme);

                    var schemes = await schemeProvider.GetAllSchemesAsync();
                    foreach (var scheme in schemes)
                    {
                        programLogger.LogInformation("Registered scheme: {Name} ({Handler})", scheme.Name, scheme.HandlerType?.FullName);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while dumping authentication schemes");
                }
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
