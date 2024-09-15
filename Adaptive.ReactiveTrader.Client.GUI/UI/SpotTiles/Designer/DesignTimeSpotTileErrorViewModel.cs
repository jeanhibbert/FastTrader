using System.Windows.Input;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles.Designer
{
    public class DesignTimeSpotTileErrorViewModel : ViewModelBase, ISpotTileErrorViewModel
    {
        public string ErrorMessage 
        {
            get
            {
                return "An error occured while executing the trade. Please check your blotter and if your position is unknown, contact your support representative.";
            }
        }
        public ICommand DismissCommand { get; private set; }
    }
}