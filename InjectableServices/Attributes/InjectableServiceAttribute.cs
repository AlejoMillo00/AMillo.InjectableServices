namespace AMillo.InjectableServices.Attributes;

using Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
public sealed class InjectableServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) : Attribute
{
    private readonly ServiceLifetime _serviceLifetime = serviceLifetime;
    public ServiceLifetime Lifetime { get { return _serviceLifetime; } }
}
