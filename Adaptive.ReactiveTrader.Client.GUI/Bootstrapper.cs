using NewWave.FastTrader.Client.Concurrency;
using NewWave.FastTrader.Client.Configuration;
using NewWave.FastTrader.Client.Domain;
using NewWave.FastTrader.Client.Domain.Instrumentation;
using NewWave.FastTrader.Client.UI.Blotter;
using NewWave.FastTrader.Client.UI.Connectivity;
using NewWave.FastTrader.Client.UI.Shell;
using NewWave.FastTrader.Client.UI.SpotTiles;
using Autofac;

namespace NewWave.FastTrader.Client
{
    public class Bootstrapper
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Domain.ReactiveTrader>().As<IReactiveTrader>().SingleInstance();
            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>();
            builder.RegisterType<UserProvider>().As<IUserProvider>();
            builder.RegisterType<ConcurrencyService>().As<IConcurrencyService>();

            // views
            builder.RegisterType<ShellView>();
            builder.RegisterType<SpotTilesView>();
            builder.RegisterType<SpotTileView>();
            builder.RegisterType<BlotterView>();
            
            // view models
            builder.RegisterType<ShellViewModel>().As<IShellViewModel>().ExternallyOwned();
            builder.RegisterType<SpotTilesViewModel>().As<ISpotTilesViewModel>().ExternallyOwned();
            builder.RegisterType<SpotTileViewModel>().As<ISpotTileViewModel>().ExternallyOwned();
            builder.RegisterType<SpotTileErrorViewModel>().As<ISpotTileErrorViewModel>().ExternallyOwned();
            builder.RegisterType<SpotTileConfigViewModel>().As<ISpotTileConfigViewModel>().ExternallyOwned();
            builder.RegisterType<SpotTilePricingViewModel>().As<ISpotTilePricingViewModel>().ExternallyOwned();
            builder.RegisterType<OneWayPriceViewModel>().As<IOneWayPriceViewModel>().ExternallyOwned();
            builder.RegisterType<SpotTileAffirmationViewModel>().As<ISpotTileAffirmationViewModel>().ExternallyOwned();
            builder.RegisterType<BlotterViewModel>().As<IBlotterViewModel>().ExternallyOwned();
            builder.RegisterType<TradeViewModel>().As<ITradeViewModel>().ExternallyOwned();
            builder.RegisterType<ConnectivityStatusViewModel>().As<IConnectivityStatusViewModel>().ExternallyOwned();

            return builder.Build();
        } 
    }
}