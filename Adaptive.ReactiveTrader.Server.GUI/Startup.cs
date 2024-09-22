using Autofac;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewWave.FastTrader.Server.Pricing;
using NewWave.FastTrader.Server.Transport;

namespace NewWave.FastTrader.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddSignalR();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your dependencies here
            builder.RegisterType<AutofacSignalRDependencyResolver>()
                   .As<DefaultDependencyResolver>()
                   .WithParameter("container", App.Container)
                   .SingleInstance();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                //app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub("/signalr");
            });
        }
    }
}