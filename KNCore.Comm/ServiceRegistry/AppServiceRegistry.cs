using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KNCore.Comm.ServiceRegistry
{
    public static class AppServiceRegistry
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            List<Type> implementationTypes = AppServiceTypeFinder.FindFromCompileLibraries();
            RegisterAppServices(services, implementationTypes);
        }

        public static void RegisterAppServices(this IServiceCollection services, Assembly assembly)
        {
            List<Type> implementationTypes = AppServiceTypeFinder.Find(assembly);
            RegisterAppServices(services, implementationTypes);
        }

        public static void RegisterAppServicesFromDirectory(this IServiceCollection services, string path)
        {
            List<Type> implementationTypes = AppServiceTypeFinder.FindFromDirectory(path);
            RegisterAppServices(services, implementationTypes);
        }

        static void RegisterAppServices(IServiceCollection services, List<Type> implementationTypes)
        {
            Type appServiceType = typeof(IAppService);
            foreach (Type implementationType in implementationTypes)
            {
                var implementedAppServiceTypes = implementationType.GetTypeInfo().ImplementedInterfaces.Where(a => a != appServiceType && appServiceType.IsAssignableFrom(a));

                foreach (Type implementedAppServiceType in implementedAppServiceTypes)
                {
                    if (typeof(IDisposable).IsAssignableFrom(implementationType))
                        services.AddScoped(implementedAppServiceType, implementationType);
                    else
                        services.AddTransient(implementedAppServiceType, implementationType);
                }
            }
        }
    }
}
