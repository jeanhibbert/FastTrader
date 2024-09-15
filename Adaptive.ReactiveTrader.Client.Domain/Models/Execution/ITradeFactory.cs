using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Client.Domain.Models.Execution
{
    interface ITradeFactory
    {
        ITrade Create(TradeDto trade);
    }
}