using System.Reactive.Concurrency;

namespace NewWave.FastTrader.Client.Concurrency
{
    public interface IConcurrencyService
    {
        IScheduler Dispatcher { get; }
        IScheduler DispatcherPeriodic { get; }
        IScheduler ThreadPool { get; }
    }
}