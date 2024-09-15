namespace NewWave.FastTrader.Shared.Extensions
{
    public interface IStale<out T>
    {
        bool IsStale { get; }
        T Update { get; }
    }
}