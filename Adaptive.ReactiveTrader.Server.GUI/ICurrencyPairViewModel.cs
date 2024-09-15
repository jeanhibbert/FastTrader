using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Server
{
    public interface ICurrencyPairViewModel : IViewModel
    {
        string Symbol { get; }
        bool Available { get; set; }
        bool Stale { get; set; }
        string Comment { get; }
    }
}