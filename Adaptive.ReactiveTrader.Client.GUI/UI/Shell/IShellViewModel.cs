using NewWave.FastTrader.Client.UI.Blotter;
using NewWave.FastTrader.Client.UI.Connectivity;
using NewWave.FastTrader.Client.UI.SpotTiles;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.Shell
{
    public interface IShellViewModel : IViewModel
    {
        ISpotTilesViewModel SpotTiles { get; }
        IBlotterViewModel Blotter { get; }
        IConnectivityStatusViewModel ConnectivityStatus { get; }
    }
}
