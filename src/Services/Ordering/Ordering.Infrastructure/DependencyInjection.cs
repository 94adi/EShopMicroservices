using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, opt) =>
            {
                opt.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                opt.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
