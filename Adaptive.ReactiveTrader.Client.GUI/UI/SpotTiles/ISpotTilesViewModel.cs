using System.Collections.ObjectModel;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    public interface ISpotTilesViewModel : IViewModel
    {
        ObservableCollection<ISpotTileViewModel> SpotTiles { get; } 
    }
}
