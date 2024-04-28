namespace AMillo.InjectableServices.Extensions;

using AMillo.InjectableServices.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

internal static class TypeExtension
{
    internal static Type[] GetInjectableInterfaces(this Type[] types)
    {
        return types
            .Where(t => t.IsInterface && t.IsDefined(typeof(InjectableServiceAttribute), false))
            .ToArray();
    }

    internal static Type[] GetImplementationsForInterface(this Type[] types, Type @interface)
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

    internal static ServiceLifetime GetLifetimeFromAttributeData(this Type type)
    {
        IList<CustomAttributeData> attributesData = type.GetCustomAttributesData();

        CustomAttributeData injectableAttributeData = attributesData
            .First(a => a.AttributeType == typeof(InjectableServiceAttribute));

        CustomAttributeTypedArgument lifetimeArgument = injectableAttributeData
            .ConstructorArguments
            .First(a => a.ArgumentType == typeof(ServiceLifetime));

        return (ServiceLifetime)(lifetimeArgument.Value ?? ServiceLifetime.Scoped);
    }
}
