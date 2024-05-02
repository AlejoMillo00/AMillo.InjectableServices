using AMillo.InjectableServices.Attributes;

namespace InjectableServices.Demo;

//[InjectableService(Lifetime = ServiceLifetime.Singleton)]
//[InjectableService(Lifetime = ServiceLifetime.Transient)]
//[InjectableService(Lifetime = ServiceLifetime.Scoped)]
[InjectableService] //Scoped default
internal interface IDemoGenericService<T>
{
    string GetTypeOfGeneric();
}

internal sealed class DemoGenericService<T> : IDemoGenericService<T>
{
    public string GetTypeOfGeneric()
    {
        return typeof(T).ToString();
    }
}
