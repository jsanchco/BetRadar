namespace Codere.BetRadar.Configurations
{
    #region Using

    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Codere.BetRadar.Domain.Services;
    using Codere.BetRadar.ServiceEventsBetRadar;

    #endregion

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IServiceEvents, ServiceEventsBetRadar>();

            return services; 
        }
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = new ReferenceLoopHandling());

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build());
            });
    }
}