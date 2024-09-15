using NewWave.FastTrader.Server.Blotter;
using NewWave.FastTrader.Server.Execution;
using NewWave.FastTrader.Server.Pricing;
using NewWave.FastTrader.Server.ReferenceData;
using NewWave.FastTrader.Server.Transport;
using Autofac;

namespace NewWave.FastTrader.Server
{
    public class Bootstrapper
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            // pricing
            builder.RegisterType<PricePublisher>().As<IPricePublisher>().SingleInstance();
            builder.RegisterType<PriceFeedSimulator>().As<IPriceFeed>().SingleInstance();
            builder.RegisterType<PriceLastValueCache>().As<IPriceLastValueCache>().SingleInstance();
            builder.RegisterType<PricingHub>().SingleInstance();

            // reference data
            builder.RegisterType<CurrencyPairRepository>().As<ICurrencyPairRepository>().SingleInstance();
            builder.RegisterType<CurrencyPairUpdatePublisher>().As<ICurrencyPairUpdatePublisher>().SingleInstance();
            builder.RegisterType<ReferenceDataHub>().SingleInstance();            

            // execution            
            builder.RegisterType<ExecutionService>().As<IExecutionService>().SingleInstance();
            builder.RegisterType<ExecutionHub>().SingleInstance();            
            
            // blotter
            builder.RegisterType<BlotterPublisher>().As<IBlotterPublisher>().SingleInstance();
            builder.RegisterType<TradeRepository>().As<ITradeRepository>().SingleInstance();
            builder.RegisterType<BlotterHub>().SingleInstance();            
            
            builder.RegisterType<ContextHolder>().As<IContextHolder>().SingleInstance();

            return builder.Build();
        }
    }
}