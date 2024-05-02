using AMillo.InjectableServices.Attributes;

namespace InjectableServices.Demo;

//[InjectableService(Lifetime = ServiceLifetime.Singleton)] //For singleton
//[InjectableService(Lifetime = ServiceLifetime.Transient)] //For transient
//[InjectableService(Lifetime = ServiceLifetime.Scoped)] //For scoped (default)
[InjectableService] //Scoped default
internal interface IDemoService
{
    string GetHelloWorld();
}

internal sealed class DemoService : IDemoService
{
    public string GetHelloWorld()
    {
        return "Hello World!";
    }
}
