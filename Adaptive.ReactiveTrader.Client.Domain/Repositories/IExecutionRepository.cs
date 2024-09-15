using System;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Client.Domain.Models.Pricing;
using NewWave.FastTrader.Shared.Extensions;

namespace NewWave.FastTrader.Client.Domain.Repositories
{
    interface IExecutionRepository
    {
        IObservable<IStale<ITrade>> ExecuteRequest(IExecutablePrice executablePrice, long notional, string dealtCurrency);
    }
}