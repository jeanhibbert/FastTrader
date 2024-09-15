using System.Reactive.Concurrency;

namespace NewWave.FastTrader.Client.Domain.Concurrency
{
    internal sealed class ConcurrencyService : IConcurrencyService
    {
        public IScheduler ThreadPool
        {
            get { return ThreadPoolScheduler.Instance; }
        }
    }
}