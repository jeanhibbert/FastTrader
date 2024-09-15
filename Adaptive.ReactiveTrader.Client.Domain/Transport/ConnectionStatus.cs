namespace NewWave.FastTrader.Client.Domain.Transport
{
    public enum ConnectionStatus
    {
        Connecting,
        Connected,
        ConnectionSlow,
        Reconnecting,
        Reconnected,
        Closed,
        Uninitialized
    }
}