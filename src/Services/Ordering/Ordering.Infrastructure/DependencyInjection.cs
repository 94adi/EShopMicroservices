using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Application.Data;

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
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, opt) =>
            {
                opt.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                opt.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
