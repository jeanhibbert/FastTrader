﻿namespace NewWave.FastTrader.Client.UI.Shell
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public ShellView(IShellViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
