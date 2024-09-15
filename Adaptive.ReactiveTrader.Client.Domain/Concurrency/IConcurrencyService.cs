using System.Reactive.Concurrency;

namespace NewWave.FastTrader.Client.Domain.Concurrency
{
    internal interface IConcurrencyService
    {
         IScheduler ThreadPool { get; }
    }
}