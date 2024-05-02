namespace AMillo.InjectableServices.Extensions.DependencyInjection;

using AMillo.InjectableServices.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

public static class DependencyInjectionExtension
{
    public static void AddInjectableServices(this IServiceCollection services)
    {
        Assembly[] assemblies = AppDomain
            .CurrentDomain
            .GetAssemblies();

        services.AddInjectableServicesFromAssemblies(assemblies);
    }

    public static void AddInjectableServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        services.AddInjectableServicesFromAssemblies([assembly]);
    }

    public static void AddInjectableServicesFromAssemblies(this IServiceCollection services, Assembly[] assemblies)
    {
        Type[] types = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .ToArray();

        foreach (Type @interface in GetInjectableInterfaces(types))
        {
            InjectInterfaceWithAllImplementations(@interface, types, services);
        }
    }

    private static Type[] GetInjectableInterfaces(Type[] types)
    {
        return types
            .Where(t => t.IsInterface && t.IsDefined(typeof(InjectableServiceAttribute), false))
            .ToArray();
    }

    private static void InjectInterfaceWithAllImplementations(Type @interface, Type[] types, IServiceCollection services)
    {
        foreach (Type implementation in GetImplementationsForInterface(@interface, types))
        {
            _ = GetLifetimeFromAttributeData(@interface) switch
            {
                ServiceLifetime.Singleton => services.AddSingleton(@interface, implementation),
                ServiceLifetime.Transient => services.AddTransient(@interface, implementation),
                _ => services.AddScoped(@interface, implementation),
            };
        }
    }

    private static Type[] GetImplementationsForInterface(Type @interface, Type[] types)
    {
        if (@interface.IsGenericType)
        {
            return types.Where(t =>
                t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface)).ToArray();
        }

        return types
            .Where(t => @interface.IsAssignableFrom(t) && t.IsClass)
            .ToArray();
    }

    private static ServiceLifetime GetLifetimeFromAttributeData(Type @interface)
    {
        IList<CustomAttributeData> attributesData = @interface.GetCustomAttributesData();

        CustomAttributeData injectableAttributeData = attributesData
            .First(a => a.AttributeType == typeof(InjectableServiceAttribute));

        CustomAttributeTypedArgument lifetimeArgument = injectableAttributeData
            .ConstructorArguments
            .First(a => a.ArgumentType == typeof(ServiceLifetime));

        return (ServiceLifetime)(lifetimeArgument.Value ?? ServiceLifetime.Scoped);
    }
}
