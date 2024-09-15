using System.Windows.Input;
using NewWave.FastTrader.Client.Domain.Models;
using NewWave.FastTrader.Client.Domain.Models.Pricing;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    public interface IOneWayPriceViewModel : IViewModel
    {
        Direction Direction { get; }
        string BigFigures { get; }
        string Pips { get; }
        string TenthOfPip { get; }
        ICommand ExecuteCommand { get; }
        bool IsExecuting { get; }
        void OnPrice(IExecutablePrice executablePrice);
        void OnStalePrice();
        SpotTileExecutionMode ExecutionMode { get; set; }
    }
}
