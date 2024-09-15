using NewWave.FastTrader.Client.Domain.Models.Pricing;

namespace NewWave.FastTrader.Client.Domain.Instrumentation
{
    public interface IPriceLatencyRecorder
    {
        void OnRendered(IPrice price);
        void OnReceived(IPrice price);
        Statistics CalculateAndReset();
    }
}