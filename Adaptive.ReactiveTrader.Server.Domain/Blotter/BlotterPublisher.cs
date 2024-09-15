using System.Threading.Tasks;
using NewWave.FastTrader.Server.Transport;
using NewWave.FastTrader.Shared.DTO.Execution;
using log4net;

namespace NewWave.FastTrader.Server.Blotter
{
    public class BlotterPublisher : IBlotterPublisher
    {
        private readonly IContextHolder _contextHolder;
        private static readonly ILog Log = LogManager.GetLogger(typeof(BlotterPublisher));

        public BlotterPublisher(IContextHolder contextHolder)
        {
            _contextHolder = contextHolder;
        }

        public Task Publish(TradeDto trade)
        {
            if (_contextHolder.BlotterHubClients == null) return Task.FromResult(false);

            Log.InfoFormat("Broadcast new trade to blotters: {0}", trade);
            return _contextHolder.BlotterHubClients.Group(BlotterHub.BlotterGroupName).OnNewTrade(new []{trade});
        }
    }
}