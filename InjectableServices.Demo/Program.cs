using AMillo.InjectableServices.Extensions.DependencyInjection;
using InjectableServices.Demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Look for injectable services inside the specified assemblies
//builder.Services.AddInjectableServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

//Look for injectable services inside a single specified assembly
//builder.Services.AddInjectableServicesFromAssembly(typeof(Program).Assembly); 

//Look for injectable services inside all assemblies for current AppDomain
builder.Services.AddInjectableServices(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/demo", (IDemoService demoService) =>
{
    return demoService.GetHelloWorld();
})
.WithName("DemoService")
.WithOpenApi();

app.MapGet("/demo/generics", (IDemoGenericService<byte> demoGenericService) =>
{
    return demoGenericService.GetTypeOfGeneric();
})
.WithName("DemoGenericService")
.WithOpenApi();

app.Run();