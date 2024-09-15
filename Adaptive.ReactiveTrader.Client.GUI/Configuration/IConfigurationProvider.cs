namespace NewWave.FastTrader.Client.Configuration
{
    public interface IConfigurationProvider
    {
        string[] Servers { get; }
    }
}