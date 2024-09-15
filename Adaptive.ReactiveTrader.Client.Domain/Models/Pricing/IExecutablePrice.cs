using System;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Shared.Extensions;

namespace NewWave.FastTrader.Client.Domain.Models.Pricing
{
    public interface IExecutablePrice
    {
        IObservable<IStale<ITrade>> ExecuteRequest(long notional, string dealtCurrency);
        Direction Direction { get; }
        IPrice Parent { get; }
        decimal Rate { get; }
    }
}
