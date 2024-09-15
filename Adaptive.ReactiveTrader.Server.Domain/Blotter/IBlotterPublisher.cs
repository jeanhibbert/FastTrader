using System.Threading.Tasks;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Server.Blotter
{
    public interface IBlotterPublisher
    {
        Task Publish(TradeDto trade);
    }
}