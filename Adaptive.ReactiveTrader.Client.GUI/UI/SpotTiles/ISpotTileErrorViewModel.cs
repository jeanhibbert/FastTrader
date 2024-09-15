using System.Windows.Input;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    public interface ISpotTileErrorViewModel : IViewModel
    {
        string ErrorMessage { get; }
        ICommand DismissCommand { get; }
    }
}