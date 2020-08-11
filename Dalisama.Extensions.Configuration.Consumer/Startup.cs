using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dalisama.Extensions.Configuration.Consumer
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration = (IConfiguration)new ConfigurationBuilder().AddApiConfiguration(options =>
           {
           options.Url = "https://localhost:5001/ConfigurationProvider";
           options.ReloadOnChange = true;

               options.HttpClient = () =>
               {
                   var handler = new HttpClientHandler();
                   handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                    {
                        return errors == SslPolicyErrors.None;
                    };
                   return new HttpClient(handler);
               };

           }).Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ClassOption>(Configuration.GetSection("Section1"));
            services.Configure<ClassOptionExtend>(Configuration.GetSection("Section2"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
