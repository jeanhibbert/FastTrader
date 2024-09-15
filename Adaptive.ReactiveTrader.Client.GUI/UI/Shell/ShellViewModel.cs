using NewWave.FastTrader.Client.UI.Blotter;
using NewWave.FastTrader.Client.UI.Connectivity;
using NewWave.FastTrader.Client.UI.SpotTiles;
using NewWave.FastTrader.Shared.UI;
using PropertyChanged;

namespace NewWave.FastTrader.Client.UI.Shell
{
    [AddINotifyPropertyChangedInterface]
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ISpotTilesViewModel SpotTiles { get; private set; }
        public IBlotterViewModel Blotter { get; private set; }
        public IConnectivityStatusViewModel ConnectivityStatus { get; private set; }

        public ShellViewModel(ISpotTilesViewModel spotTiles, IBlotterViewModel blotter, IConnectivityStatusViewModel connectivityStatusViewModel)
        {
            SpotTiles = spotTiles;
            Blotter = blotter;
            ConnectivityStatus = connectivityStatusViewModel;
        }
    }
}
