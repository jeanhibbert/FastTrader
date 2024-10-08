﻿using System.Windows.Input;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    public class SpotTileErrorViewModel : ViewModelBase, ISpotTileErrorViewModel
    {
        private readonly ISpotTileViewModel _parent;

        public SpotTileErrorViewModel(ISpotTileViewModel parent, string message)
        {
            _parent = parent;
            ErrorMessage = message;

            DismissCommand = new DelegateCommand(Dismiss);
        }

        private void Dismiss()
        {
            _parent.DismissError();

        }

        public string ErrorMessage { get; private set; }
        public ICommand DismissCommand { get; private set; }
    }
}