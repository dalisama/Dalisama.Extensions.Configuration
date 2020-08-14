# Welcome to my humble readme

In this Repo, I implement a simple custom configuration provider that enable you to deliver and update your configuration via a Get api.
When you have a microservice architecture and you need to update and change configuration, it is a tedious work, you need to restart and update every microservices.
With tis NuGet, you only need to expose an APi that feed configuration to your service, and Voilà, you will be able to update your configuration in real time without the need of restarting anything.
This NuGet will support the use of Consul and any API that provide you config via Get, it supports all type of authentication, because simply you will need to provide your httpClient, so +1 for flexibility.

## Lead by Example

Let me walk you through the first example:

Let’s first download this repository locally and you will need the dotnetcore runtime duh!
After that in the solution directory run this cmd:

```
dotnet build 
```

now you need to open two command line in the following
directory:
1. \Dalisama.Extensions.Configuration.Api: this application will be providing the configuration via an api
2. \Dalisama.Extensions.Configuration.Consumer: this application will be getting his configuration from the previous application

So you need to run this command in both command-lines:
```
dotnet run 
```
from you brower, try to access this link: https://localhost:5005/confconsumer and you will get this json:
```json
[
  {
    "element1": 360,
    "element2": 454,
    "element3": 545,
    "element4": 1606
  },
  {
    "element1": 232,
    "element2": 1202,
    "element3": 757,
    "element4": 755
  }
]
```

You will notice if you refresh the link in the browser, the first element in this array will be changed but the last one will be the same, because actually it's the same object but when it's resolved with IOptionsSnapshot, the framework will fetch the last value, so here you get the flexibility of knowing the first value or the updated one.

````csharp
[HttpGet]
public List<ClassOption> Get([FromServices] IOptionsSnapshot<ClassOption> option1, [FromServices] IOptions<ClassOption> option2)
{
    return new List<ClassOption> { option1.Value, option2.Value};
}
````
To get this result all you have to do is adding the nuget: Dalisama.Extensions.Configuration and updating your startup.cs:

````csharp

   public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration = (IConfiguration)new ConfigurationBuilder().AddApiConfiguration(options =>
           {
           options.Url = "https://localhost:5001/ConfigurationProvider";
           options.ReloadOnChange = true;

               options.HttpClientFactory = () =>
               {
                   var handler = new HttpClientHandler();
                   handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                    {
                        return errors == SslPolicyErrors.None;
                    };
                   return new HttpClient(handler);
               };
               options.COnfigKeyFormatter = (key, value) => key;
               options.COnfigValueFormatter = (key, value) => value;

           }).Build();
        }

````
and here in the ConfigureServices:
````csharp

         public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ClassOption>(Configuration.GetSection("Section1"));

        }

````

and Voilà!!!

