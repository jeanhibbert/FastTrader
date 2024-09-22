
using Microsoft.AspNet.SignalR.Hubs;

namespace NewWave.FastTrader.Server.Transport
{
    public interface IContextHolder
    {
        IHubCallerConnectionContext<dynamic> PricingHubClient { get; set; }
        IHubCallerConnectionContext<dynamic> BlotterHubClients { get; set; }
        IHubCallerConnectionContext<dynamic> ReferenceDataHubClients { get; set; }
    }
}