<a name="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">InjectableServices</h3>

  <p align="center">
    <a href="https://github.com/AlejoMillo00/AMillo.InjectableServices/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/AlejoMillo00/AMillo.InjectableServices/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

## About The Project

Are you tired of large Program.cs / Startup.cs files, with lots and lots of service dependency injections? Well...You are lucky!

InjectableServices is a simple feature that allows you to register your services without having to add them to the Program / Startup file, keeping them clean and smooth.

Here's why this is good:
* Cleaner and readable Program / Startup files, keep them small.
* Make your services ready-to-use just as you finish creating them, you don't even need to go into the Program / Startup file.

## Getting Started
### Installation
- .NET CLI
  ```sh
  dotnet add package AMillo.InjectableServices --version 2.0.0
  ```
- Package Manager
  ```sh
  Install-Package AMillo.InjectableServices -Version 2.0.0
  ```
### Usage
1. Add the following <strong>using</strong> directive on your Program.cs / Startup.cs file
   ```sh
   using AMillo.InjectableServices.Extensions.DependencyInjection;
   ```
2. Call the <strong>AddInjectableServices</strong> extension method using one of the following overloads
   - AddInjectableServices()
     ```sh
     //Look for injectable services inside all assemblies for current AppDomain
     builder.Services.AddInjectableServices(); 
     ```
   - AddInjectableServicesFromAssemblies(Assembly[] assemblies)
     ```sh
     //Look for injectable services inside the specified assemblies
     builder.Services.AddInjectableServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
     ```
   - AddInjectableServicesFromAssembly(Assembly assembly)
     ```sh
     //Look for injectable services inside a single specified assembly
     builder.Services.AddInjectableServicesFromAssembly(typeof(Program).Assembly); 
     ```

3. Mark your service interface with the <strong>[InjectableService]</strong> attribute.
   ```sh
    [InjectableService] //Scoped by default
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
   ```
   
4. That's it! Your service will get registered automatically at startup.

### Service Lifetime
By default, the <strong>[InjectableService]</strong> attribute will register your services as <strong>"Scoped"</strong>. 
<br />
But if you want, you can specify the lifetime for your service as follows: 
<br/>

1. Mark your service interface with the <strong>[InjectableService]</strong> attribute passing the lifetime to the attribute's constructor.
   ```sh
    //[InjectableService(Lifetime = ServiceLifetime.Singleton)] //For singleton
    //[InjectableService(Lifetime = ServiceLifetime.Transient)] //For transient
    [InjectableService(Lifetime = ServiceLifetime.Scoped)] //For scoped (default)
    internal interface IDemoService
    {
        string GetHelloWorld();
    }
   ```
2. That's it! Now your service will get register as Transient, Singleton or Scoped automatically on startup.

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Contact

Alejo Millo - alejo.millo@outlook.com

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/AlejoMillo00/AMillo.InjectableServices.svg?style=for-the-badge
[contributors-url]: https://github.com/AlejoMillo00/AMillo.InjectableServices/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/AlejoMillo00/AMillo.InjectableServices.svg?style=for-the-badge
[forks-url]: https://github.com/AlejoMillo00/AMillo.InjectableServices/network/members
[stars-shield]: https://img.shields.io/github/stars/AlejoMillo00/AMillo.InjectableServices.svg?style=for-the-badge
[stars-url]: https://github.com/AlejoMillo00/AMillo.InjectableServices/stargazers
[issues-shield]: https://img.shields.io/github/issues/AlejoMillo00/AMillo.InjectableServices.svg?style=for-the-badge
[issues-url]: https://github.com/AlejoMillo00/AMillo.InjectableServices/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/alejo-millo-77371a196/
