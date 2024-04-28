namespace AMillo.InjectableServices.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtension
{
    private static IServiceCollection _services = new ServiceCollection();
    private static Type[] _types = [];

    public static void AddInjectableServices(this IServiceCollection services)
    {
        Initialize(services);
        InjectServices();
    }
    private static void Initialize(IServiceCollection services)
    {
        _types = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .ToArray();
        _services = services;
    }

    private static void InjectServices()
    {
        foreach (Type @interface in _types.GetInjectableInterfaces())
        {
            Inject(@interface);
        }
    }

    private static void Inject(Type @interface)
    {
        foreach (Type implementation in _types.GetImplementationsForInterface(@interface))
        {
            _ = @interface.GetLifetimeFromAttributeData() switch
            {
                ServiceLifetime.Singleton => _services.AddSingleton(@interface, implementation),
                ServiceLifetime.Transient => _services.AddTransient(@interface, implementation),
                _ => _services.AddScoped(@interface, implementation),
            };
        }
    }
}
