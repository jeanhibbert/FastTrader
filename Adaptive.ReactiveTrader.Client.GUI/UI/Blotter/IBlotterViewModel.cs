using System.Collections.ObjectModel;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.Blotter
{
    public interface IBlotterViewModel : IViewModel
    {
        ObservableCollection<ITradeViewModel> Trades { get; } 
    }
}
