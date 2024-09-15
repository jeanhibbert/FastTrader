﻿using NewWave.FastTrader.Server.Pricing;
using NewWave.FastTrader.Server.Transport;
using Autofac;
using Microsoft.AspNet.SignalR;
using Owin;

namespace NewWave.FastTrader.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();

            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Build();
            var priceFeed = container.Resolve<IPriceFeed>();
            priceFeed.Start();

            var hubConfiguration = new HubConfiguration
            {
                // you don't want to use that in prod, just when debugging
                EnableDetailedErrors = true,
                Resolver = new AutofacSignalRDependencyResolver(container)
            };

            app.MapSignalR(hubConfiguration);
        }
    }
}