﻿using System.Collections.ObjectModel;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles.Designer
{
    public class DesignTimeSpotTilesViewModel : ViewModelBase, ISpotTilesViewModel
    {
        public DesignTimeSpotTilesViewModel()
        {
            SpotTiles = new ObservableCollection<ISpotTileViewModel>
            {
                new DesignTimeSpotTileViewModel(),
                new DesignTimeSpotTileViewModel(),
                new DesignTimeSpotTileViewModel(),
                new DesignTimeSpotTileViewModel(),
                new DesignTimeSpotTileViewModel()
            };
        }

        public ObservableCollection<ISpotTileViewModel> SpotTiles { get; private set; }
    }
}