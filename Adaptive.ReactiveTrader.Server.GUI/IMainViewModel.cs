using System.Collections.ObjectModel;
using System.Windows.Input;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Server
{
    public interface IMainViewModel : IViewModel
    {
        ICommand StartStopCommand { get; }
        string ServerStatus { get; }
        string StartStopCommandText { get; }
        string Throughput { get; }
        string DesiredThroughput { get; }
        int UpdateFrequency { get; set; }
        ObservableCollection<ICurrencyPairViewModel> CurrencyPairs { get; }
        void Start();
    }
}