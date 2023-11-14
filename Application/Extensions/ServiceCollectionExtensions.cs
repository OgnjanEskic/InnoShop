using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// It is used to extend the ServiceCollection class so that additional "extension" 
        /// methods can be written and used to simplify the Dependency Injection configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.
                RegisterServicesFromAssemblies(typeof(ServiceCollectionExtensions).Assembly));
            return services;
        }
    }
}
