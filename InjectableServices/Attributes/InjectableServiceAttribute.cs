namespace AMillo.InjectableServices.Attributes;

using Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
public sealed class InjectableServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
{
    private readonly ServiceLifetime _lifetime = lifetime;
    public ServiceLifetime Lifetime { get { return _lifetime; } }
}
